﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp_WCF.UserServ {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User_MODEL", Namespace="http://schemas.datacontract.org/2004/07/KANG.MODEL")]
    [System.SerializableAttribute()]
    public partial class User_MODEL : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AgeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
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
        public int Age {
            get {
                return this.AgeField;
            }
            set {
                if ((this.AgeField.Equals(value) != true)) {
                    this.AgeField = value;
                    this.RaisePropertyChanged("Age");
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
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServ.IUserOperateOf_User_MODEL")]
    public interface IUserOperateOf_User_MODEL {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserOperateOf_User_MODEL/Insert", ReplyAction="http://tempuri.org/IUserOperateOf_User_MODEL/InsertResponse")]
        bool Insert(ConsoleApp_WCF.UserServ.User_MODEL t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserOperateOf_User_MODEL/Insert", ReplyAction="http://tempuri.org/IUserOperateOf_User_MODEL/InsertResponse")]
        System.Threading.Tasks.Task<bool> InsertAsync(ConsoleApp_WCF.UserServ.User_MODEL t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserOperateOf_User_MODEL/GetList", ReplyAction="http://tempuri.org/IUserOperateOf_User_MODEL/GetListResponse")]
        ConsoleApp_WCF.UserServ.User_MODEL[] GetList(string where, int pageIndex, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserOperateOf_User_MODEL/GetList", ReplyAction="http://tempuri.org/IUserOperateOf_User_MODEL/GetListResponse")]
        System.Threading.Tasks.Task<ConsoleApp_WCF.UserServ.User_MODEL[]> GetListAsync(string where, int pageIndex, int pageSize);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserOperateOf_User_MODELChannel : ConsoleApp_WCF.UserServ.IUserOperateOf_User_MODEL, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserOperateOf_User_MODELClient : System.ServiceModel.ClientBase<ConsoleApp_WCF.UserServ.IUserOperateOf_User_MODEL>, ConsoleApp_WCF.UserServ.IUserOperateOf_User_MODEL {
        
        public UserOperateOf_User_MODELClient() {
        }
        
        public UserOperateOf_User_MODELClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserOperateOf_User_MODELClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserOperateOf_User_MODELClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserOperateOf_User_MODELClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Insert(ConsoleApp_WCF.UserServ.User_MODEL t) {
            return base.Channel.Insert(t);
        }
        
        public System.Threading.Tasks.Task<bool> InsertAsync(ConsoleApp_WCF.UserServ.User_MODEL t) {
            return base.Channel.InsertAsync(t);
        }
        
        public ConsoleApp_WCF.UserServ.User_MODEL[] GetList(string where, int pageIndex, int pageSize) {
            return base.Channel.GetList(where, pageIndex, pageSize);
        }
        
        public System.Threading.Tasks.Task<ConsoleApp_WCF.UserServ.User_MODEL[]> GetListAsync(string where, int pageIndex, int pageSize) {
            return base.Channel.GetListAsync(where, pageIndex, pageSize);
        }
    }
}
