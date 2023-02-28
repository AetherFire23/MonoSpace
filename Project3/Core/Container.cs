using MonoGame.Extended.Collections;
using Project3.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public class Container
    {
        protected Dictionary<Type, object> _instances = new Dictionary<Type, object>();


        protected List<Type> _registeredTypes = new List<Type>();
        private Dictionary<Type, List<Type>> _typeAndConstructorParameters { get; set; } = new();

        private Dictionary<Type, List<FieldInfo>> _instantiableFields { get; set; } = new Dictionary<Type, List<FieldInfo>>();

        public Container()
        {

        }

        public Container(List<Type> registeredTypes, Dictionary<Type, object> instances,
           Dictionary<Type, List<FieldInfo>> factory2)
        {
            _registeredTypes = registeredTypes;
            _instances = instances;
            _instantiableFields = factory2;
        }

        public void Register<T>()
        {
            _registeredTypes.Add(typeof(T));
        }


        public void RegisterInFactory<T>() where T : class
        {
            var fieldsInjection = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<InjectAttribute>() is not null).ToList();

            _instantiableFields.Add(typeof(T), fieldsInjection);
        }

        public T Resolve<T>() where T : class
        {
            var instance = _instances.GetValueOrDefault(typeof(T));

            if (instance is null) throw new ArgumentNullException($"This type {typeof(T)} was not registered.");

            return (T)instance;
        }

        public GameObject ResolveFromRegisteredTypes(GameObject gameObject)
        {
            var dependencies = _instantiableFields.GetValueOrDefault(gameObject.GetType());

            if (!dependencies.Any()) throw new ArgumentNullException($"the following type : {gameObject.GetType()} was not registered for factories.");

            foreach (var dependencyType in dependencies)
            {
                object instance = Resolve(dependencyType.FieldType);

                dependencyType.SetValue(gameObject, instance);
            }

            return gameObject;
        }

        public void BuildInjection()
        {
            RegisterContainer();
            _typeAndConstructorParameters = CreateTypeAndParameterMap(_registeredTypes);
            CheckMissingDependencies();
            CheckForCascadingDependencies();
            ActivateAllConstructorless();
            ActivateWithConstructors();
            InstantiateGameObjects();
        }

        public void RegisterContainer()
        {
            _registeredTypes.Add(typeof(Container));
            _instances.Add(typeof(Container), this);
        }

        private void ActivateWithConstructors()
        {
            var typesWithConstructors = _registeredTypes.Where(x => !_instances.Keys.Contains(x)).ToList();

            foreach (var type in typesWithConstructors)
            {
                if (_instances.ContainsKey(type)) continue;

                ActivateAndFulfillDependencies(type);
            }
        }

        private object Resolve(Type type)
        {
            var instance = _instances.GetValueOrDefault(type);

            if (instance is null) throw new ArgumentNullException($"This type {type} was not registered.");

            return instance;
        }

        private void ActivateAndFulfillDependencies(Type t)
        {
            var dependencies = _typeAndConstructorParameters.GetValueOrDefault(t);
            var missingTypes = dependencies.Where(d => !_instances.ContainsKey(d)).ToList();

            if (missingTypes.Any())
            {
                foreach (var missing in missingTypes)
                {
                    ActivateAndFulfillDependencies(missing);
                }
            }

            FetchDependenciesAndActivate(t);
        }

        private void ActivateAllConstructorless()
        {
            var notActivated = _typeAndConstructorParameters.Where(x => !_instances.ContainsKey(x.Key));

            foreach (KeyValuePair<Type, List<Type>> typeAndParameters in notActivated)
            {
                if (typeAndParameters.Value.Any()) continue;

                object instance = Activator.CreateInstance(typeAndParameters.Key);
                _instances.Add(typeAndParameters.Key, instance);
            }
        }

        private void FetchDependenciesAndActivate(Type t)
        {
            var dependencies = _typeAndConstructorParameters.GetValueOrDefault(t);
            object[] activatedDependencies = dependencies.Select(x => _instances.GetValueOrDefault(x)).ToArray();
            object instance = Activator.CreateInstance(t, activatedDependencies);
            _instances.Add(t, instance);
        }

        private List<Type> GetConstructorParameterTypes(Type type)
        {
            var parameterDependencies = type.GetConstructors()
                .First()
                .GetParameters()
                .ToList()
                .Select(p => p.ParameterType).ToList();

            return parameterDependencies;
        }

        private void CheckMissingDependencies()
        {
            var flattenedParameters = _typeAndConstructorParameters.Values
                .Where(x => x.Any())
                .SelectMany(x => x)
                .ToList();

            foreach (var parameter in flattenedParameters)
            {
                bool missingDependency = !_registeredTypes.Contains(parameter);

                if (missingDependency) throw new Exception($"Some dependency is missing : {parameter}");
            }
        }

        private void CheckForCascadingDependencies()
        {
            foreach (KeyValuePair<Type, List<Type>> typeAndParameters in _typeAndConstructorParameters)
            {
                foreach (Type parameter in typeAndParameters.Value)
                {
                    var inverseDependencies = _typeAndConstructorParameters.GetValueOrDefault(parameter);
                    bool isCascading = inverseDependencies.Contains(typeAndParameters.Key);
                    if (isCascading) throw new Exception($"Cascade detected. `{typeAndParameters.Key}` and {parameter} mutually reference each other");
                }
            }
        }

        private Dictionary<Type, List<Type>> CreateTypeAndParameterMap(List<Type> types)
        {
            var map = new Dictionary<Type, List<Type>>();

            foreach (Type type in types)
            {
                List<Type> dependencies = GetConstructorParameterTypes(type);
                map.Add(type, dependencies);
            }
            return map;
        }

        private void InstantiateGameObjects()
        {
            var gameObjects = _instances
                .Where(x => x.Key.IsAssignableTo(typeof(GameObject)))
                .Select(x => x.Value as GameObject)
                .ToList();

            foreach (var gameObject in gameObjects)
            {
                Entities.Instantiate(gameObject);
            }
        }
    }
}