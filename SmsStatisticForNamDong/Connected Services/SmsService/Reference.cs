﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmsStatisticForNamDong.SmsService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="NmnMsg", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class NmnMsg : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TenKhachHangField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MsgBodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string id {
            get {
                return this.idField;
            }
            set {
                if ((object.ReferenceEquals(this.idField, value) != true)) {
                    this.idField = value;
                    this.RaisePropertyChanged("id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string TenKhachHang {
            get {
                return this.TenKhachHangField;
            }
            set {
                if ((object.ReferenceEquals(this.TenKhachHangField, value) != true)) {
                    this.TenKhachHangField = value;
                    this.RaisePropertyChanged("TenKhachHang");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string MsgBody {
            get {
                return this.MsgBodyField;
            }
            set {
                if ((object.ReferenceEquals(this.MsgBodyField, value) != true)) {
                    this.MsgBodyField = value;
                    this.RaisePropertyChanged("MsgBody");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Phone {
            get {
                return this.PhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneField, value) != true)) {
                    this.PhoneField = value;
                    this.RaisePropertyChanged("Phone");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://tempuri.org/", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SmsService.SmsBrandnameSoap")]
    public interface SmsBrandnameSoap {
        
        // CODEGEN: Generating message contract since element name msg from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GuiMotHoacNhieuTin", ReplyAction="*")]
        SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinResponse GuiMotHoacNhieuTin(SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinRequest request);
        
        // CODEGEN: Generating message contract since element name tenKhachHang from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GuiMotNoiDungNhieuSo", ReplyAction="*")]
        SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoResponse GuiMotNoiDungNhieuSo(SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoRequest request);
        
        // CODEGEN: Generating message contract since element name adminUsername from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DayDuLieu", ReplyAction="*")]
        SmsStatisticForNamDong.SmsService.DayDuLieuResponse DayDuLieu(SmsStatisticForNamDong.SmsService.DayDuLieuRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GuiMotHoacNhieuTinRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GuiMotHoacNhieuTin", Namespace="http://tempuri.org/", Order=0)]
        public SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinRequestBody Body;
        
        public GuiMotHoacNhieuTinRequest() {
        }
        
        public GuiMotHoacNhieuTinRequest(SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GuiMotHoacNhieuTinRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SmsStatisticForNamDong.SmsService.NmnMsg[] msg;
        
        public GuiMotHoacNhieuTinRequestBody() {
        }
        
        public GuiMotHoacNhieuTinRequestBody(SmsStatisticForNamDong.SmsService.NmnMsg[] msg) {
            this.msg = msg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GuiMotHoacNhieuTinResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GuiMotHoacNhieuTinResponse", Namespace="http://tempuri.org/", Order=0)]
        public SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinResponseBody Body;
        
        public GuiMotHoacNhieuTinResponse() {
        }
        
        public GuiMotHoacNhieuTinResponse(SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GuiMotHoacNhieuTinResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SmsStatisticForNamDong.SmsService.ArrayOfString GuiMotHoacNhieuTinResult;
        
        public GuiMotHoacNhieuTinResponseBody() {
        }
        
        public GuiMotHoacNhieuTinResponseBody(SmsStatisticForNamDong.SmsService.ArrayOfString GuiMotHoacNhieuTinResult) {
            this.GuiMotHoacNhieuTinResult = GuiMotHoacNhieuTinResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GuiMotNoiDungNhieuSoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GuiMotNoiDungNhieuSo", Namespace="http://tempuri.org/", Order=0)]
        public SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoRequestBody Body;
        
        public GuiMotNoiDungNhieuSoRequest() {
        }
        
        public GuiMotNoiDungNhieuSoRequest(SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GuiMotNoiDungNhieuSoRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string tenKhachHang;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string phone;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string msg;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int id;
        
        public GuiMotNoiDungNhieuSoRequestBody() {
        }
        
        public GuiMotNoiDungNhieuSoRequestBody(string tenKhachHang, string phone, string msg, string username, string password, int id) {
            this.tenKhachHang = tenKhachHang;
            this.phone = phone;
            this.msg = msg;
            this.username = username;
            this.password = password;
            this.id = id;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GuiMotNoiDungNhieuSoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GuiMotNoiDungNhieuSoResponse", Namespace="http://tempuri.org/", Order=0)]
        public SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoResponseBody Body;
        
        public GuiMotNoiDungNhieuSoResponse() {
        }
        
        public GuiMotNoiDungNhieuSoResponse(SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GuiMotNoiDungNhieuSoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SmsStatisticForNamDong.SmsService.ArrayOfString GuiMotNoiDungNhieuSoResult;
        
        public GuiMotNoiDungNhieuSoResponseBody() {
        }
        
        public GuiMotNoiDungNhieuSoResponseBody(SmsStatisticForNamDong.SmsService.ArrayOfString GuiMotNoiDungNhieuSoResult) {
            this.GuiMotNoiDungNhieuSoResult = GuiMotNoiDungNhieuSoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DayDuLieuRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DayDuLieu", Namespace="http://tempuri.org/", Order=0)]
        public SmsStatisticForNamDong.SmsService.DayDuLieuRequestBody Body;
        
        public DayDuLieuRequest() {
        }
        
        public DayDuLieuRequest(SmsStatisticForNamDong.SmsService.DayDuLieuRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class DayDuLieuRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string adminUsername;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string adminPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string name;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string reqId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string labelId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string contractId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string contractTypeId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string templateId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string num;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string content;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string scheduleTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string mobileList;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string isTelcoSub;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string agentId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=14)]
        public string apiUser;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=15)]
        public string apiPass;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=16)]
        public string username;
        
        public DayDuLieuRequestBody() {
        }
        
        public DayDuLieuRequestBody(
                    string adminUsername, 
                    string adminPassword, 
                    string name, 
                    string reqId, 
                    string labelId, 
                    string contractId, 
                    string contractTypeId, 
                    string templateId, 
                    string num, 
                    string content, 
                    string scheduleTime, 
                    string mobileList, 
                    string isTelcoSub, 
                    string agentId, 
                    string apiUser, 
                    string apiPass, 
                    string username) {
            this.adminUsername = adminUsername;
            this.adminPassword = adminPassword;
            this.name = name;
            this.reqId = reqId;
            this.labelId = labelId;
            this.contractId = contractId;
            this.contractTypeId = contractTypeId;
            this.templateId = templateId;
            this.num = num;
            this.content = content;
            this.scheduleTime = scheduleTime;
            this.mobileList = mobileList;
            this.isTelcoSub = isTelcoSub;
            this.agentId = agentId;
            this.apiUser = apiUser;
            this.apiPass = apiPass;
            this.username = username;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DayDuLieuResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DayDuLieuResponse", Namespace="http://tempuri.org/", Order=0)]
        public SmsStatisticForNamDong.SmsService.DayDuLieuResponseBody Body;
        
        public DayDuLieuResponse() {
        }
        
        public DayDuLieuResponse(SmsStatisticForNamDong.SmsService.DayDuLieuResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class DayDuLieuResponseBody {
        
        public DayDuLieuResponseBody() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SmsBrandnameSoapChannel : SmsStatisticForNamDong.SmsService.SmsBrandnameSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SmsBrandnameSoapClient : System.ServiceModel.ClientBase<SmsStatisticForNamDong.SmsService.SmsBrandnameSoap>, SmsStatisticForNamDong.SmsService.SmsBrandnameSoap {
        
        public SmsBrandnameSoapClient() {
        }
        
        public SmsBrandnameSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SmsBrandnameSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsBrandnameSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsBrandnameSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinResponse SmsStatisticForNamDong.SmsService.SmsBrandnameSoap.GuiMotHoacNhieuTin(SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinRequest request) {
            return base.Channel.GuiMotHoacNhieuTin(request);
        }
        
        public SmsStatisticForNamDong.SmsService.ArrayOfString GuiMotHoacNhieuTin(SmsStatisticForNamDong.SmsService.NmnMsg[] msg) {
            SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinRequest inValue = new SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinRequest();
            inValue.Body = new SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinRequestBody();
            inValue.Body.msg = msg;
            SmsStatisticForNamDong.SmsService.GuiMotHoacNhieuTinResponse retVal = ((SmsStatisticForNamDong.SmsService.SmsBrandnameSoap)(this)).GuiMotHoacNhieuTin(inValue);
            return retVal.Body.GuiMotHoacNhieuTinResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoResponse SmsStatisticForNamDong.SmsService.SmsBrandnameSoap.GuiMotNoiDungNhieuSo(SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoRequest request) {
            return base.Channel.GuiMotNoiDungNhieuSo(request);
        }
        
        public SmsStatisticForNamDong.SmsService.ArrayOfString GuiMotNoiDungNhieuSo(string tenKhachHang, string phone, string msg, string username, string password, int id) {
            SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoRequest inValue = new SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoRequest();
            inValue.Body = new SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoRequestBody();
            inValue.Body.tenKhachHang = tenKhachHang;
            inValue.Body.phone = phone;
            inValue.Body.msg = msg;
            inValue.Body.username = username;
            inValue.Body.password = password;
            inValue.Body.id = id;
            SmsStatisticForNamDong.SmsService.GuiMotNoiDungNhieuSoResponse retVal = ((SmsStatisticForNamDong.SmsService.SmsBrandnameSoap)(this)).GuiMotNoiDungNhieuSo(inValue);
            return retVal.Body.GuiMotNoiDungNhieuSoResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SmsStatisticForNamDong.SmsService.DayDuLieuResponse SmsStatisticForNamDong.SmsService.SmsBrandnameSoap.DayDuLieu(SmsStatisticForNamDong.SmsService.DayDuLieuRequest request) {
            return base.Channel.DayDuLieu(request);
        }
        
        public void DayDuLieu(
                    string adminUsername, 
                    string adminPassword, 
                    string name, 
                    string reqId, 
                    string labelId, 
                    string contractId, 
                    string contractTypeId, 
                    string templateId, 
                    string num, 
                    string content, 
                    string scheduleTime, 
                    string mobileList, 
                    string isTelcoSub, 
                    string agentId, 
                    string apiUser, 
                    string apiPass, 
                    string username) {
            SmsStatisticForNamDong.SmsService.DayDuLieuRequest inValue = new SmsStatisticForNamDong.SmsService.DayDuLieuRequest();
            inValue.Body = new SmsStatisticForNamDong.SmsService.DayDuLieuRequestBody();
            inValue.Body.adminUsername = adminUsername;
            inValue.Body.adminPassword = adminPassword;
            inValue.Body.name = name;
            inValue.Body.reqId = reqId;
            inValue.Body.labelId = labelId;
            inValue.Body.contractId = contractId;
            inValue.Body.contractTypeId = contractTypeId;
            inValue.Body.templateId = templateId;
            inValue.Body.num = num;
            inValue.Body.content = content;
            inValue.Body.scheduleTime = scheduleTime;
            inValue.Body.mobileList = mobileList;
            inValue.Body.isTelcoSub = isTelcoSub;
            inValue.Body.agentId = agentId;
            inValue.Body.apiUser = apiUser;
            inValue.Body.apiPass = apiPass;
            inValue.Body.username = username;
            SmsStatisticForNamDong.SmsService.DayDuLieuResponse retVal = ((SmsStatisticForNamDong.SmsService.SmsBrandnameSoap)(this)).DayDuLieu(inValue);
        }
    }
}
