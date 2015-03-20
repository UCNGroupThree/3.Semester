﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightAdmin.AdministratorService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Administrator", Namespace="http://schemas.datacontract.org/2004/07/WCFService.Model", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Administrator : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ConcurrencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordHashField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Concurrency {
            get {
                return this.ConcurrencyField;
            }
            set {
                if ((object.ReferenceEquals(this.ConcurrencyField, value) != true)) {
                    this.ConcurrencyField = value;
                    this.RaisePropertyChanged("Concurrency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PasswordHash {
            get {
                return this.PasswordHashField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordHashField, value) != true)) {
                    this.PasswordHashField = value;
                    this.RaisePropertyChanged("PasswordHash");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AdministratorService.IAdministratorService")]
    public interface IAdministratorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/AddAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/AddAdministratorResponse")]
        int AddAdministrator(FlightAdmin.AdministratorService.Administrator administrator);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/AddAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/AddAdministratorResponse")]
        System.Threading.Tasks.Task<int> AddAdministratorAsync(FlightAdmin.AdministratorService.Administrator administrator);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/UpdateAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/UpdateAdministratorResponse")]
        FlightAdmin.AdministratorService.Administrator UpdateAdministrator(FlightAdmin.AdministratorService.Administrator administrator);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/UpdateAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/UpdateAdministratorResponse")]
        System.Threading.Tasks.Task<FlightAdmin.AdministratorService.Administrator> UpdateAdministratorAsync(FlightAdmin.AdministratorService.Administrator administrator);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/DeleteAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/DeleteAdministratorResponse")]
        void DeleteAdministrator(FlightAdmin.AdministratorService.Administrator administrator);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/DeleteAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/DeleteAdministratorResponse")]
        System.Threading.Tasks.Task DeleteAdministratorAsync(FlightAdmin.AdministratorService.Administrator administrator);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/GetAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/GetAdministratorResponse")]
        FlightAdmin.AdministratorService.Administrator GetAdministrator(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdministratorService/GetAdministrator", ReplyAction="http://tempuri.org/IAdministratorService/GetAdministratorResponse")]
        System.Threading.Tasks.Task<FlightAdmin.AdministratorService.Administrator> GetAdministratorAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdministratorServiceChannel : FlightAdmin.AdministratorService.IAdministratorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AdministratorServiceClient : System.ServiceModel.ClientBase<FlightAdmin.AdministratorService.IAdministratorService>, FlightAdmin.AdministratorService.IAdministratorService {
        
        public AdministratorServiceClient() {
        }
        
        public AdministratorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AdministratorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdministratorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdministratorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int AddAdministrator(FlightAdmin.AdministratorService.Administrator administrator) {
            return base.Channel.AddAdministrator(administrator);
        }
        
        public System.Threading.Tasks.Task<int> AddAdministratorAsync(FlightAdmin.AdministratorService.Administrator administrator) {
            return base.Channel.AddAdministratorAsync(administrator);
        }
        
        public FlightAdmin.AdministratorService.Administrator UpdateAdministrator(FlightAdmin.AdministratorService.Administrator administrator) {
            return base.Channel.UpdateAdministrator(administrator);
        }
        
        public System.Threading.Tasks.Task<FlightAdmin.AdministratorService.Administrator> UpdateAdministratorAsync(FlightAdmin.AdministratorService.Administrator administrator) {
            return base.Channel.UpdateAdministratorAsync(administrator);
        }
        
        public void DeleteAdministrator(FlightAdmin.AdministratorService.Administrator administrator) {
            base.Channel.DeleteAdministrator(administrator);
        }
        
        public System.Threading.Tasks.Task DeleteAdministratorAsync(FlightAdmin.AdministratorService.Administrator administrator) {
            return base.Channel.DeleteAdministratorAsync(administrator);
        }
        
        public FlightAdmin.AdministratorService.Administrator GetAdministrator(int id) {
            return base.Channel.GetAdministrator(id);
        }
        
        public System.Threading.Tasks.Task<FlightAdmin.AdministratorService.Administrator> GetAdministratorAsync(int id) {
            return base.Channel.GetAdministratorAsync(id);
        }
    }
}