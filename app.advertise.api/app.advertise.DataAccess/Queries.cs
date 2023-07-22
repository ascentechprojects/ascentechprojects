﻿namespace app.advertise.DataAccess
{
    internal static class Queries
    {
        internal const string Schema_Adevertisement = "advertisement";
        internal const string Schema_Admin = "admins";

        internal const string SP_HoardTyConfig_Ins = "advertisement.aoad_hoardtyconfig_ins";
        internal const string SP_HoardTypeMaster_Ins = "advertisement.aoad_hoardingtype_ins";
        internal const string SP_DisplayTypeMaster_Ins = "advertisement.AOAD_DISPLAYTYPE_INS";
        internal const string SP_LocationMaster_Ins = "advertisement.AOAD_LOCATION_INS";
        internal const string SP_HoardingMaster_Ins = "advertisement.AOAD_Hording_ins";
        internal const string SP_Aoad_Appli_Auth_Search_Result = "advertisement.aoad_appli_auth_search_result";
        internal const string SP_AppliCloseAuth_Ins = "advertisement.aoad_AppliCloseAuth_ins";
        internal const string SP_DisplayTypeConfig_Ins = "advertisement.AOAD_DISPLAYTYPE_CONFIG_INS";
        internal const string SP_AppliClose_Ins = "advertisement.aoad_AppliClose_ins";

        internal const string List_Active_HoardTypeConfig = $"Select NUM_HOARDINGCONFIG_ID,NUM_HOARDINGCONFIG_HOARDID,NUM_HOARDINGCONFIG_ULBID,VAR_HOARDINGCONFIG_INSBY,DAT_HOARDINGCONFIG_INSDT,VAR_HOARDINGCONFIG_UPDBY,DAT_HOARDINGCONFIG_UPDT,VAR_HOARDINGCONFIG_ACTIVEFLAG from {Schema_Adevertisement}.AOAD_HOARDINGTYPE_CONFIG Where VAR_HOARDINGCONFIG_ACTIVEFLAG='Y'";
        internal const string Single_HoardTypeConfig = "Select Top 1 NUM_HOARDINGCONFIG_ID,NUM_HOARDINGCONFIG_HOARDID,NUM_HOARDINGCONFIG_ULBID,VAR_HOARDINGCONFIG_INSBY,DAT_HOARDINGCONFIG_INSDT,VAR_HOARDINGCONFIG_UPDBY,DAT_HOARDINGCONFIG_UPDT,VAR_HOARDINGCONFIG_ACTIVEFLAG from advertisement.AOAD_HOARDINGTYPE_CONFIG Where NUM_HOARDINGCONFIG_ID={id}";
        internal const string List_HOARDINGTYPE_MST = $"Select NUM_HOARDINGTYPE_ID,VAR_HOARDINGTYPE_NAME,VAR_HOARDINGTYPE_STATUS,VAR_HOARDINGTYPE_INSBY,DAT_HOARDINGTYPE_INSDT,VAR_HOARDINGTYPE_UPDBY,DAT_HOARDINGTYPE_UPDT,VAR_HOARDINGTYPE_IPADDRESS from AOAD_HOARDINGTYPE_MST";
        internal const string Single_HOARDINGTYPE_MST = "Select NUM_HOARDINGTYPE_ID,VAR_HOARDINGTYPE_NAME,VAR_HOARDINGTYPE_STATUS,VAR_HOARDINGTYPE_INSBY,DAT_HOARDINGTYPE_INSDT,VAR_HOARDINGTYPE_UPDBY,DAT_HOARDINGTYPE_UPDT,VAR_HOARDINGTYPE_IPADDRESS FROM AOAD_HOARDINGTYPE_MST WHERE NUM_HOARDINGTYPE_ID=:id";
        internal const string List_DisplayTYPE_MST = " SELECT NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_INSBY,DAT_DISPLAYTYPE_INSDT,VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT,VAR_DISPLAYTYPE_IPADDRESS From aoad_displaytype_mst ORDER By NUM_DISPLAYTYPE_ID DESC";
        internal const string Single_ById_DisplayTYPE_MST = "SELECT NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_INSBY,DAT_DISPLAYTYPE_INSDT,VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT,VAR_DISPLAYTYPE_IPADDRESS From aoad_displaytype_mst WHERE NUM_DISPLAYTYPE_ID=:id";
        internal const string List_location_MST = "Select alm.NUM_LOCATION_ULBID,alm.NUM_LOCATION_ID,alm.VAR_LOCATION_NAME,alm.NUM_LOCATION_AREA,alm.NUM_LOCATION_PINCODE,alm.VAR_LOCATION_ACTIVE,alm.NUM_LOCATION_LATITUDE,alm.NUM_LOCATION_LONGITUDE,alm.NUM_LOCATION_PRABHAGID,aprm.var_prabhag_name from aoad_location_mst alm LEFT JOIN  aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=alm.num_location_prabhagid Where alm.num_location_ulbid=:ulbid";
        internal const string Single_ById_location_MST = "Select NUM_LOCATION_ULBID,NUM_LOCATION_ID,VAR_LOCATION_NAME,NUM_LOCATION_AREA,NUM_LOCATION_PINCODE,VAR_LOCATION_ACTIVE,VAR_LOCATION_INSBY,DAT_LOCATION_INSDT,VAR_LOCATION_UPDBY,DAT_LOCATION_UPDT,VAR_LOCATION_IPADDRESS,NUM_LOCATION_LATITUDE,NUM_LOCATION_LONGITUDE,NUM_LOCATION_PRABHAGID from aoad_location_mst Where NUM_LOCATION_ID=:id";
        internal const string ModifyStatus_DisplayType_MST = "UPDATE Advertisement.AOAD_DISPLAYTYPE_MST Set VAR_DISPLAYTYPE_STATUS=:VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_UPDBY=:VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT=:DAT_DISPLAYTYPE_UPDT Where NUM_DISPLAYTYPE_ID=:NUM_DISPLAYTYPE_ID";
        internal const string SelectAll_HoardingMaster = "SELECT  ahm.NUM_HORDING_ULBID, ahm.NUM_HORDING_ID, ahm.VAR_HORDING_HOLDNAME, ahm.VAR_HORDING_HOLDADDRESS, ahm.VAR_HORDING_HOLDTYPE, adm.VAR_DISPLAYTYPE_NAME, aprm.VAR_PRABHAG_NAME, alm.VAR_LOCATION_NAME, ahm.VAR_HORDING_LANDMARK, ahm.NUM_HORDING_LENGTH, ahm.NUM_HORDING_WIDTH, ahm.NUM_HORDING_TOTALSQFT, ahm.VAR_HORDING_ACTIVE, ahm.DAT_HORDING_INSDT, ahm.VAR_HORDING_INSBY, ahm.VAR_HORDING_UPDBY, ahm.DAT_HORDING_UPDT,ahm.VAR_HORDING_OWNERSHIP, ahtm.VAR_HOARDINGTYPE_NAME FROM aoad_hording_mst ahm LEFT JOIN aoad_displaytype_mst adm ON adm.NUM_DISPLAYTYPE_ID=ahm.NUM_HORDING_DISPTYPEID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=ahm.NUM_HORDING_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=ahm.NUM_HORDING_LOCATIONID LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE Where ahm.NUM_HORDING_ULBID=:ulbId ORDER BY ahm.NUM_HORDING_ID DESC";
        internal const string Select_byId_HoardingMaster = "SELECT NUM_HORDING_ULBID, NUM_HORDING_ID, VAR_HORDING_HOLDNAME, VAR_HORDING_HOLDADDRESS, VAR_HORDING_HOLDTYPE, NUM_HORDING_DISPTYPEID, NUM_HORDING_PRABHAGID, NUM_HORDING_LOCATIONID, VAR_HORDING_LANDMARK, NUM_HORDING_LENGTH, NUM_HORDING_WIDTH, NUM_HORDING_TOTALSQFT, VAR_HORDING_ACTIVE,  DAT_HORDING_INSDT, VAR_HORDING_INSBY, VAR_HORDING_UPDBY, DAT_HORDING_UPDT,VAR_HORDING_OWNERSHIP FROM aoad_hording_mst WHERE NUM_HORDING_ID=:id AND NUM_HORDING_ULBID=:ulbId";
        internal const string ModifyStatus_HoardingMaster = "UPDATE Advertisement.aoad_hording_mst Set VAR_HORDING_ACTIVE=:VAR_HORDING_ACTIVE,VAR_HORDING_UPDBY=:VAR_HORDING_UPDBY,DAT_HORDING_UPDT=:DAT_HORDING_UPDT Where NUM_HORDING_ID=:NUM_HORDING_ID AND NUM_HORDING_ULBID=:NUM_HORDING_ULBID";
        internal const string Select_Active_DisplayTypes = "Select NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS from aoad_displaytype_mst where VAR_DISPLAYTYPE_STATUS='A'";
        internal const string Select_DisplayTypes_Exists_Config = "Select adm.NUM_DISPLAYTYPE_ID,adm.VAR_DISPLAYTYPE_NAME,adc.NUM_DISPLAYCONFIG_DISPLAYID,adc.NUM_DISPLAYCONFIG_ID, CASE WHEN adc.NUM_DISPLAYCONFIG_DISPLAYID IS NULL THEN 0 ELSE 1 END AS ExistsInConfig from aoad_displaytype_mst adm LEFT JOIN aoad_displaytype_config adc ON adm.NUM_DISPLAYTYPE_ID = adc.NUM_DISPLAYCONFIG_DISPLAYID AND adc.NUM_DISPLAYCONFIG_ULBID=1 where adm.VAR_DISPLAYTYPE_STATUS='A'";
        internal const string ModifyStatus_LocationMaster = "UPDATE Advertisement.aoad_location_mst Set VAR_LOCATION_ACTIVE=:VAR_LOCATION_ACTIVE,VAR_LOCATION_UPDBY=:VAR_LOCATION_UPDBY,DAT_LOCATION_UPDT=:DAT_LOCATION_UPDT Where NUM_LOCATION_ID=:NUM_LOCATION_ID AND NUM_LOCATION_ULBID=:NUM_LOCATION_ULBID";


        internal const string ListItem_HordingTypes = "Select VAR_HOARDINGTYPE_NAME As DisplayName,NUM_HOARDINGTYPE_ID As Id,VAR_HOARDINGTYPE_STATUS as Active from  aoad_hoardingtype_mst where VAR_HOARDINGTYPE_STATUS='A'";
        internal const string ListItem_Locations = "select NUM_LOCATION_ID As Id, VAR_LOCATION_NAME As DisplayName from aoad_location_mst where VAR_LOCATION_ACTIVE='A'";
        internal const string ListItem_DisplayTypes = "Select NUM_DISPLAYTYPE_ID As Id ,VAR_DISPLAYTYPE_NAME As DisplayName,VAR_DISPLAYTYPE_STATUS as Active from aoad_displaytype_mst where VAR_DISPLAYTYPE_STATUS='A'";
        internal const string ListItem_Prabhags = "SELECT NUM_PRABHAG_ID As Id, VAR_PRABHAG_NAME As DisplayName from aoad_prabhag_mas where VAR_PRABHAG_STATUS='Y'";
        internal const string ListItem_Locations_By_PrabhagId = "SELECT NUM_LOCATION_ID As Id,VAR_LOCATION_NAME As DisplayName FROM aoad_location_mst WHERE VAR_LOCATION_ACTIVE='A' AND NUM_LOCATION_PRABHAGID=:prabhagId";

        internal const string Application_Auth_Search = "SELECT DISTINCT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.VAR_APPLI_INSBY,apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT, apm.VAR_APPLI_UPDBY,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME,adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID WHERE apm.NUM_APPLI_LOCATIONID = :p_location_id AND apm.NUM_APPLI_PRABHAGID = :p_prabhag_id AND apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.VAR_APPLI_APPROVFLAG='P' ORDER BY apm.DAT_APPLI_INSDT DESC";
        internal const string Application_Details_By_AppliId = "SELECT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.VAR_APPLI_INSBY,apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT, apm.VAR_APPLI_UPDBY,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME,adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID WHERE  apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.NUM_APPLI_ID=:p_appli_id";
        internal const string Application_Deauth_Search = "SELECT DISTINCT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.VAR_APPLI_INSBY,apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT, apm.VAR_APPLI_UPDBY,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME,adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID WHERE apm.NUM_APPLI_LOCATIONID = :p_location_id AND apm.NUM_APPLI_PRABHAGID = :p_prabhag_id AND apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.VAR_APPLI_APPROVFLAG IN ('A','R') ORDER BY apm.DAT_APPLI_INSDT DESC";
        internal const string ApplicationClose_By_AppId = "Select NUM_APPLICLOSE_ID,NUM_APPLICLOSE_HORDING_ID,VAR_APPLICLOSE_REMARK,BLO_APPLICLOSE_IMAGE,VAR_APPLICLOSE_INSBY,DAT_APPLICLOSE_INSDT,VAR_APPLICLOSE_APPROVBY,DAT_APPLICLOSE_APPROVDT,VAR_APPLICLOSE_APPROVREMARK,VAR_APPLICLOSE_IPADDRESS,NUM_APPLICLOSE_APPID,NUM_APPLICLOSE_ULBID From aoad_appliclose_mst Where NUM_APPLICLOSE_ULBID=:NUM_APPLICLOSE_ULBID AND NUM_APPLICLOSE_APPID=:NUM_APPLICLOSE_APPID";
    }
}
