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
        internal const string List_DisplayTYPE_MST = " SELECT NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_INSBY,DAT_DISPLAYTYPE_INSDT,VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT,VAR_DISPLAYTYPE_IPADDRESS From aoad_displaytype_mst";
        internal const string Single_ById_DisplayTYPE_MST = "SELECT NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_INSBY,DAT_DISPLAYTYPE_INSDT,VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT,VAR_DISPLAYTYPE_IPADDRESS From aoad_displaytype_mst WHERE NUM_DISPLAYTYPE_ID=:id";
        internal const string List_location_MST = "Select NUM_LOCATION_ULBID,NUM_LOCATION_ID,VAR_LOCATION_NAME,NUM_LOCATION_AREA,NUM_LOCATION_PINCODE,VAR_LOCATION_ACTIVE,VAR_LOCATION_INSBY,DAT_LOCATION_INSDT,VAR_LOCATION_UPDBY,DAT_LOCATION_UPDT,VAR_LOCATION_IPADDRESS,NUM_LOCATION_LATITUDE,NUM_LOCATION_LONGITUDE,NUM_LOCATION_PRABHAGID from aoad_location_mst";
        internal const string Single_ById_location_MST = "Select NUM_LOCATION_ULBID,NUM_LOCATION_ID,VAR_LOCATION_NAME,NUM_LOCATION_AREA,NUM_LOCATION_PINCODE,VAR_LOCATION_ACTIVE,VAR_LOCATION_INSBY,DAT_LOCATION_INSDT,VAR_LOCATION_UPDBY,DAT_LOCATION_UPDT,VAR_LOCATION_IPADDRESS,NUM_LOCATION_LATITUDE,NUM_LOCATION_LONGITUDE,NUM_LOCATION_PRABHAGID from aoad_location_mst Where NUM_LOCATION_ULBID={id}";
        internal const string ModifyStatus_DisplayType_MST = "UPDATE Advertisement.AOAD_DISPLAYTYPE_MST Set VAR_DISPLAYTYPE_STATUS=:VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_UPDBY=:VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT=:DAT_DISPLAYTYPE_UPDT Where NUM_DISPLAYTYPE_ID=:NUM_DISPLAYTYPE_ID";
        internal const string SelectAll_HoardingMaster = "SELECT NUM_HORDING_ULBID, NUM_HORDING_ID, VAR_HORDING_HOLDNAME, VAR_HORDING_HOLDADDRESS, VAR_HORDING_HOLDTYPE, NUM_HORDING_DISPTYPEID, NUM_HORDING_PRABHAGID, NUM_HORDING_LOCATIONID, VAR_HORDING_LANDMARK, NUM_HORDING_LENGTH, NUM_HORDING_WIDTH, NUM_HORDING_TOTALSQFT, VAR_HORDING_ACTIVE,  DAT_HORDING_INSDT, VAR_HORDING_INSBY, VAR_HORDING_UPDBY, DAT_HORDING_UPDT,VAR_HORDING_OWNERSHIP FROM aoad_hording_mst";
        internal const string Select_byId_HoardingMaster = "SELECT NUM_HORDING_ULBID, NUM_HORDING_ID, VAR_HORDING_HOLDNAME, VAR_HORDING_HOLDADDRESS, VAR_HORDING_HOLDTYPE, NUM_HORDING_DISPTYPEID, NUM_HORDING_PRABHAGID, NUM_HORDING_LOCATIONID, VAR_HORDING_LANDMARK, NUM_HORDING_LENGTH, NUM_HORDING_WIDTH, NUM_HORDING_TOTALSQFT, VAR_HORDING_ACTIVE,  DAT_HORDING_INSDT, VAR_HORDING_INSBY, VAR_HORDING_UPDBY, DAT_HORDING_UPDT,VAR_HORDING_OWNERSHIP FROM aoad_hording_mst WHERE NUM_HORDING_ID=:id";
        internal const string ModifyStatus_HoardingMaster = "UPDATE Advertisement.aoad_hording_mst Set VAR_HORDING_ACTIVE=:VAR_HORDING_ACTIVE,VAR_HORDING_UPDBY=:VAR_HORDING_UPDBY,DAT_HORDING_UPDT=:DAT_HORDING_UPDT Where NUM_HORDING_ID=:NUM_HORDING_ID";
        internal const string Select_Active_DisplayTypes = "Select NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS from aoad_displaytype_mst where VAR_DISPLAYTYPE_STATUS='A'";
        internal const string Select_DisplayTypes_Exists_Config = "Select adm.NUM_DISPLAYTYPE_ID,adm.VAR_DISPLAYTYPE_NAME,adc.NUM_DISPLAYCONFIG_DISPLAYID,adc.NUM_DISPLAYCONFIG_ID, CASE WHEN adc.NUM_DISPLAYCONFIG_DISPLAYID IS NULL THEN 0 ELSE 1 END AS ExistsInConfig from aoad_displaytype_mst adm LEFT JOIN aoad_displaytype_config adc ON adm.NUM_DISPLAYTYPE_ID = adc.NUM_DISPLAYCONFIG_DISPLAYID AND adc.NUM_DISPLAYCONFIG_ULBID=1 where adm.VAR_DISPLAYTYPE_STATUS='A'";


        internal const string ListItem_HordingTypes = "Select VAR_HOARDINGTYPE_NAME As DisplayName,NUM_HOARDINGTYPE_ID As Id,VAR_HOARDINGTYPE_STATUS as Active from  aoad_hoardingtype_mst where VAR_HOARDINGTYPE_STATUS='A'";
        internal const string ListItem_Locations = "select NUM_LOCATION_ID As Id, VAR_LOCATION_NAME As DisplayName from aoad_location_mst where VAR_LOCATION_ACTIVE='A'";
        internal const string ListItem_DisplayTypes = "Select NUM_DISPLAYTYPE_ID As Id ,VAR_DISPLAYTYPE_NAME As DisplayName,VAR_DISPLAYTYPE_STATUS as Active from aoad_displaytype_mst where VAR_DISPLAYTYPE_STATUS='A'";
        internal const string ListItem_Prabhags = "SELECT NUM_PRABHAG_ID As Id, VAR_PRABHAG_NAME As DisplayName from aoad_prabhag_mas where VAR_PRABHAG_STATUS='Y'";
        internal const string ListItem_Locations_By_PrabhagId = "SELECT NUM_LOCATION_ID As Id,VAR_LOCATION_NAME As DisplayName FROM aoad_location_mst WHERE VAR_LOCATION_ACTIVE='A' AND NUM_LOCATION_PRABHAGID=:prabhagId";

        internal const string Application_Auth_Search = "SELECT apm.*,ahm.*  FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID  WHERE ahm.num_hording_locationid = :p_location_id AND ahm.num_hording_prabhagid = :p_prabhag_id";
        internal const string Application_Details_By_AppliId = "SELECT apm.*,ahm.*,aprm.VAR_PRABHAG_NAME,alocm.VAR_LOCATION_NAME,adm.VAR_DISPLAYTYPE_NAME   FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID  LEFT JOIN aoad_location_mst alocm ON alocm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID  LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID  WHERE apm.NUM_APPLI_ID=:p_appli_id";
        internal const string Application_Deauth_Search = "SELECT apm.*,ahm.*  FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID  WHERE ahm.num_hording_locationid = :p_location_id AND ahm.num_hording_prabhagid = :p_prabhag_id";


    }
}
