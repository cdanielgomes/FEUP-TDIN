using System;
using System.Collections;
using System.Runtime.Remoting;

namespace Client {
    internal static class RemoteNew {
        private static Hashtable _types;

        private static void InitTypeTable() {
            _types = new Hashtable();
            foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
                _types.Add(entry.ObjectType ?? throw new InvalidOperationException(), entry);
        }

        public static object New(Type type) {
            if (_types == null) InitTypeTable();
            WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry) _types[type];
            
            if (entry == null) 
                throw new RemotingException("Type not found!");
            
            return RemotingServices.Connect(type, entry.ObjectUrl);
        }
    }
}