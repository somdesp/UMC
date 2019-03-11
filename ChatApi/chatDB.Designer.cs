﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("SignalRModel", "FK_message_user", "user", RelationshipMultiplicity.ZeroOrOne, typeof(ChatApi.user), "message", RelationshipMultiplicity.Many, typeof(ChatApi.message), true)]
[assembly: EdmRelationshipAttribute("SignalRModel", "FK_online_user", "user", RelationshipMultiplicity.ZeroOrOne, typeof(ChatApi.user), "useractivity", RelationshipMultiplicity.Many, typeof(ChatApi.useractivity), true)]

#endregion

namespace ChatApi
{
    #region Contexts

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class SignalREntities : ObjectContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new SignalREntities object using the connection string found in the 'SignalREntities' section of the application configuration file.
        /// </summary>
        public SignalREntities() : base("name=SignalREntities", "SignalREntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initialize a new SignalREntities object.
        /// </summary>
        public SignalREntities(string connectionString) : base(connectionString, "SignalREntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initialize a new SignalREntities object.
        /// </summary>
        public SignalREntities(EntityConnection connection) : base(connection, "SignalREntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        #endregion

        #region Partial Methods

        partial void OnContextCreated();

        #endregion

        #region ObjectSet Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<message> messages
        {
            get
            {
                if ((_messages == null))
                {
                    _messages = base.CreateObjectSet<message>("messages");
                }
                return _messages;
            }
        }
        private ObjectSet<message> _messages;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<user> users
        {
            get
            {
                if ((_users == null))
                {
                    _users = base.CreateObjectSet<user>("users");
                }
                return _users;
            }
        }
        private ObjectSet<user> _users;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<useractivity> useractivities
        {
            get
            {
                if ((_useractivities == null))
                {
                    _useractivities = base.CreateObjectSet<useractivity>("useractivities");
                }
                return _useractivities;
            }
        }
        private ObjectSet<useractivity> _useractivities;

        #endregion

        #region AddTo Methods

        /// <summary>
        /// Deprecated Method for adding a new object to the messages EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTomessages(message message)
        {
            base.AddObject("messages", message);
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the users EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTousers(user user)
        {
            base.AddObject("users", user);
        }

        /// <summary>
        /// Deprecated Method for adding a new object to the useractivities EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTouseractivities(useractivity useractivity)
        {
            base.AddObject("useractivities", useractivity);
        }

        #endregion

    }

    #endregion

    #region Entities

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "SignalRModel", Name = "message")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class message : EntityObject
    {
        #region Factory Method

        /// <summary>
        /// Create a new message object.
        /// </summary>
        /// <param name="messageID">Initial value of the MessageID property.</param>
        public static message Createmessage(global::System.Int32 messageID)
        {
            message message = new message();
            message.MessageID = messageID;
            return message;
        }

        #endregion

        #region Simple Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Int32 MessageID
        {
            get
            {
                return _MessageID;
            }
            set
            {
                if (_MessageID != value)
                {
                    OnMessageIDChanging(value);
                    ReportPropertyChanging("MessageID");
                    _MessageID = StructuralObject.SetValidValue(value, "MessageID");
                    ReportPropertyChanged("MessageID");
                    OnMessageIDChanged();
                }
            }
        }
        private global::System.Int32 _MessageID;
        partial void OnMessageIDChanging(global::System.Int32 value);
        partial void OnMessageIDChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                OnUserIDChanging(value);
                ReportPropertyChanging("UserID");
                _UserID = StructuralObject.SetValidValue(value, true, "UserID");
                ReportPropertyChanged("UserID");
                OnUserIDChanged();
            }
        }
        private global::System.String _UserID;
        partial void OnUserIDChanging(global::System.String value);
        partial void OnUserIDChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String Msg
        {
            get
            {
                return _Msg;
            }
            set
            {
                OnMsgChanging(value);
                ReportPropertyChanging("Msg");
                _Msg = StructuralObject.SetValidValue(value, true, "Msg");
                ReportPropertyChanged("Msg");
                OnMsgChanged();
            }
        }
        private global::System.String _Msg;
        partial void OnMsgChanging(global::System.String value);
        partial void OnMsgChanged();

        #endregion

        #region Navigation Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("SignalRModel", "FK_message_user", "user")]
        public user user
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<user>("SignalRModel.FK_message_user", "user").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<user>("SignalRModel.FK_message_user", "user").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<user> userReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<user>("SignalRModel.FK_message_user", "user");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<user>("SignalRModel.FK_message_user", "user", value);
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "SignalRModel", Name = "user")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class user : EntityObject
    {
        #region Factory Method

        /// <summary>
        /// Create a new user object.
        /// </summary>
        /// <param name="userID">Initial value of the UserID property.</param>
        public static user Createuser(global::System.String userID)
        {
            user user = new user();
            user.UserID = userID;
            return user;
        }

        #endregion

        #region Simple Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                if (_UserID != value)
                {
                    OnUserIDChanging(value);
                    ReportPropertyChanging("UserID");
                    _UserID = StructuralObject.SetValidValue(value, false, "UserID");
                    ReportPropertyChanged("UserID");
                    OnUserIDChanged();
                }
            }
        }
        private global::System.String _UserID;
        partial void OnUserIDChanging(global::System.String value);
        partial void OnUserIDChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                OnUserNameChanging(value);
                ReportPropertyChanging("UserName");
                _UserName = StructuralObject.SetValidValue(value, true, "UserName");
                ReportPropertyChanged("UserName");
                OnUserNameChanged();
            }
        }
        private global::System.String _UserName;
        partial void OnUserNameChanging(global::System.String value);
        partial void OnUserNameChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String Password
        {
            get
            {
                return _Password;
            }
            set
            {
                OnPasswordChanging(value);
                ReportPropertyChanging("Password");
                _Password = StructuralObject.SetValidValue(value, true, "Password");
                ReportPropertyChanged("Password");
                OnPasswordChanged();
            }
        }
        private global::System.String _Password;
        partial void OnPasswordChanging(global::System.String value);
        partial void OnPasswordChanged();

        #endregion

        #region Navigation Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("SignalRModel", "FK_message_user", "message")]
        public EntityCollection<message> messages
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<message>("SignalRModel.FK_message_user", "message");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<message>("SignalRModel.FK_message_user", "message", value);
                }
            }
        }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("SignalRModel", "FK_online_user", "useractivity")]
        public EntityCollection<useractivity> useractivities
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<useractivity>("SignalRModel.FK_online_user", "useractivity");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<useractivity>("SignalRModel.FK_online_user", "useractivity", value);
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "SignalRModel", Name = "useractivity")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class useractivity : EntityObject
    {
        #region Factory Method

        /// <summary>
        /// Create a new useractivity object.
        /// </summary>
        /// <param name="onlineID">Initial value of the OnlineID property.</param>
        public static useractivity Createuseractivity(global::System.Int32 onlineID)
        {
            useractivity useractivity = new useractivity();
            useractivity.OnlineID = onlineID;
            return useractivity;
        }

        #endregion

        #region Simple Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Int32 OnlineID
        {
            get
            {
                return _OnlineID;
            }
            set
            {
                if (_OnlineID != value)
                {
                    OnOnlineIDChanging(value);
                    ReportPropertyChanging("OnlineID");
                    _OnlineID = StructuralObject.SetValidValue(value, "OnlineID");
                    ReportPropertyChanged("OnlineID");
                    OnOnlineIDChanged();
                }
            }
        }
        private global::System.Int32 _OnlineID;
        partial void OnOnlineIDChanging(global::System.Int32 value);
        partial void OnOnlineIDChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                OnUserIDChanging(value);
                ReportPropertyChanging("UserID");
                _UserID = StructuralObject.SetValidValue(value, true, "UserID");
                ReportPropertyChanged("UserID");
                OnUserIDChanged();
            }
        }
        private global::System.String _UserID;
        partial void OnUserIDChanging(global::System.String value);
        partial void OnUserIDChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String ConnectionID
        {
            get
            {
                return _ConnectionID;
            }
            set
            {
                OnConnectionIDChanging(value);
                ReportPropertyChanging("ConnectionID");
                _ConnectionID = StructuralObject.SetValidValue(value, true, "ConnectionID");
                ReportPropertyChanged("ConnectionID");
                OnConnectionIDChanged();
            }
        }
        private global::System.String _ConnectionID;
        partial void OnConnectionIDChanging(global::System.String value);
        partial void OnConnectionIDChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String Status
        {
            get
            {
                return _Status;
            }
            set
            {
                OnStatusChanging(value);
                ReportPropertyChanging("Status");
                _Status = StructuralObject.SetValidValue(value, true, "Status");
                ReportPropertyChanged("Status");
                OnStatusChanged();
            }
        }
        private global::System.String _Status;
        partial void OnStatusChanging(global::System.String value);
        partial void OnStatusChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String Avater
        {
            get
            {
                return _Avater;
            }
            set
            {
                OnAvaterChanging(value);
                ReportPropertyChanged("Avater");
                _Avater = StructuralObject.SetValidValue(value, true, "Avater");
                ReportPropertyChanged("Avater");
                OnAvaterChanged();
            }
        }
        private global::System.String _Avater;
        partial void OnAvaterChanging(global::System.String value);
        partial void OnAvaterChanged();

        #endregion

        #region Navigation Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("SignalRModel", "FK_online_user", "user")]
        public user user
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<user>("SignalRModel.FK_online_user", "user").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<user>("SignalRModel.FK_online_user", "user").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<user> userReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<user>("SignalRModel.FK_online_user", "user");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<user>("SignalRModel.FK_online_user", "user", value);
                }
            }
        }

        #endregion

    }

    #endregion

}