using System.ComponentModel;

namespace Tsi.Erp.TestTracker.Api.Security
{
    public enum Permissions
    {
        [Description("Access to read list of users")]
        User_Read,
        [Description("Access to read and modify list of users")]
        User_ReadWrite,
        [Description("Access to enable and disable all users accounts")]
        User_EnableDisableAccount,
        [Description("Access to read list of groups")]
        Group_Read,
        [Description("Access to read and modify list of users")]
        Group_ReadWrite,
        [Description("Access to read list of comments")]
        Comment_Read,
        [Description("Access to read and modify list of comments")]
        Comment_ReadWrite,
        [Description("Access to read list of files")]
        File_Read,
        [Description("Access to read and modify list of files")]
        File_ReadWrite,
        [Description("Access to read  list of functionalities")]
        Funtionality_Read,
        [Description("Access to read and modify list of functionalities")]
        Functionality_ReadWrite,
        [Description("Access to read  list of submenus")]
        Submenu_Read,
        [Description("Access to read and modify list of submenus")]
        Submenu_ReadWrite,
        [Description("Access to read  list of Hangfire")]
        Hangfire_Read,
        [Description("Access to read list of modules")]
        Module_Read,
        [Description("Access to read and modify list of modules")]
        Module_ReadWrite,
        [Description("Access to read  list of monitorings")]
        Monitoring_Read,
        [Description("Access to read and modify list of monitorings")]
        Monitoring_ReadWrite,
        [Description("Access to read  list of tickets")]
        Ticket_Read,
        [Description("Access to read and modify list of tickets")]
        Ticket_ReadWrite,
        [Description("Access to read  list of settings")]
        Settings_Read,
        [Description("Access to read and modify list of settings")]
        Settings_ReadWrite,
        [Description("Access to read and modify list of QueryStore")]
        QueryStore_Read,
        QueryStore_ReadWrite,
        [Description("Access to read  list of features")]
        Feature_Read,
        [Description("Access to read and modify list of features")]
        Feature_ReadWrite,
        [Description("Access to read and modify list of labels")]
        Labels_Read,
        [Description("Access to read list of labels")]
        Labels_ReadWrite,
    }
}
