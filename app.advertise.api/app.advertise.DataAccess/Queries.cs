﻿namespace app.advertise.DataAccess
{
    internal static partial class Queries
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
        internal const string List_HOARDINGTYPE_MST = $"Select NUM_HOARDINGTYPE_ID,VAR_HOARDINGTYPE_NAME,VAR_HOARDINGTYPE_STATUS,VAR_HOARDINGTYPE_INSBY,DAT_HOARDINGTYPE_INSDT,VAR_HOARDINGTYPE_UPDBY,DAT_HOARDINGTYPE_UPDT,VAR_HOARDINGTYPE_IPADDRESS from AOAD_HOARDINGTYPE_MST ORDER BY NUM_HOARDINGTYPE_ID DESC";
        internal const string Single_HOARDINGTYPE_MST = "Select NUM_HOARDINGTYPE_ID,VAR_HOARDINGTYPE_NAME,VAR_HOARDINGTYPE_STATUS,VAR_HOARDINGTYPE_INSBY,DAT_HOARDINGTYPE_INSDT,VAR_HOARDINGTYPE_UPDBY,DAT_HOARDINGTYPE_UPDT,VAR_HOARDINGTYPE_IPADDRESS FROM AOAD_HOARDINGTYPE_MST WHERE NUM_HOARDINGTYPE_ID=:id";
        internal const string List_DisplayTYPE_MST = " SELECT NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_INSBY,DAT_DISPLAYTYPE_INSDT,VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT,VAR_DISPLAYTYPE_IPADDRESS From aoad_displaytype_mst ORDER By NUM_DISPLAYTYPE_ID DESC";
        internal const string Single_ById_DisplayTYPE_MST = "SELECT NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_INSBY,DAT_DISPLAYTYPE_INSDT,VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT,VAR_DISPLAYTYPE_IPADDRESS From aoad_displaytype_mst WHERE NUM_DISPLAYTYPE_ID=:id";
        internal const string List_location_MST = "Select alm.NUM_LOCATION_ULBID,alm.NUM_LOCATION_ID,alm.VAR_LOCATION_NAME,alm.NUM_LOCATION_AREA,alm.NUM_LOCATION_PINCODE,alm.VAR_LOCATION_ACTIVE,alm.NUM_LOCATION_LATITUDE,alm.NUM_LOCATION_LONGITUDE,alm.NUM_LOCATION_PRABHAGID,aprm.var_prabhag_name from aoad_location_mst alm LEFT JOIN  aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=alm.num_location_prabhagid Where alm.num_location_ulbid=:ulbid ORDER BY alm.NUM_LOCATION_ID DESC";
        internal const string Single_ById_location_MST = "Select NUM_LOCATION_ULBID,NUM_LOCATION_ID,VAR_LOCATION_NAME,NUM_LOCATION_AREA,NUM_LOCATION_PINCODE,VAR_LOCATION_ACTIVE,VAR_LOCATION_INSBY,DAT_LOCATION_INSDT,VAR_LOCATION_UPDBY,DAT_LOCATION_UPDT,VAR_LOCATION_IPADDRESS,NUM_LOCATION_LATITUDE,NUM_LOCATION_LONGITUDE,NUM_LOCATION_PRABHAGID from aoad_location_mst Where NUM_LOCATION_ID=:id AND NUM_LOCATION_ULBID=:ulbId";
        internal const string ModifyStatus_DisplayType_MST = "UPDATE Advertisement.AOAD_DISPLAYTYPE_MST Set VAR_DISPLAYTYPE_STATUS=:VAR_DISPLAYTYPE_STATUS,VAR_DISPLAYTYPE_UPDBY=:VAR_DISPLAYTYPE_UPDBY,DAT_DISPLAYTYPE_UPDT=:DAT_DISPLAYTYPE_UPDT Where NUM_DISPLAYTYPE_ID=:NUM_DISPLAYTYPE_ID";
        internal const string SelectAll_HoardingMaster = "SELECT  ahm.NUM_HORDING_ULBID, ahm.NUM_HORDING_ID, ahm.VAR_HORDING_HOLDNAME, ahm.VAR_HORDING_HOLDADDRESS, ahm.VAR_HORDING_HOLDTYPE, adm.VAR_DISPLAYTYPE_NAME, aprm.VAR_PRABHAG_NAME, alm.VAR_LOCATION_NAME, ahm.VAR_HORDING_LANDMARK, ahm.NUM_HORDING_LENGTH, ahm.NUM_HORDING_WIDTH, ahm.NUM_HORDING_TOTALSQFT, ahm.VAR_HORDING_ACTIVE, ahm.DAT_HORDING_INSDT, ahm.VAR_HORDING_INSBY, ahm.VAR_HORDING_UPDBY, ahm.DAT_HORDING_UPDT,ahm.VAR_HORDING_OWNERSHIP, ahtm.VAR_HOARDINGTYPE_NAME FROM aoad_hording_mst ahm LEFT JOIN aoad_displaytype_mst adm ON adm.NUM_DISPLAYTYPE_ID=ahm.NUM_HORDING_DISPTYPEID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=ahm.NUM_HORDING_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=ahm.NUM_HORDING_LOCATIONID LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE Where ahm.NUM_HORDING_ULBID=:ulbId ORDER BY ahm.NUM_HORDING_ID DESC";
        //internal const string Select_byId_HoardingMaster = "SELECT ahm.NUM_HORDING_ULBID, ahm.NUM_HORDING_ID, ahm.VAR_HORDING_HOLDNAME, ahm.VAR_HORDING_HOLDADDRESS, ahm.VAR_HORDING_HOLDTYPE, ahm.NUM_HORDING_DISPTYPEID, ahm.NUM_HORDING_PRABHAGID, ahm.NUM_HORDING_LOCATIONID, ahm.VAR_HORDING_LANDMARK, ahm.NUM_HORDING_LENGTH, ahm.NUM_HORDING_WIDTH, ahm.NUM_HORDING_TOTALSQFT, ahm.VAR_HORDING_ACTIVE, ahm.DAT_HORDING_INSDT, ahm.VAR_HORDING_INSBY, ahm.VAR_HORDING_UPDBY, ahm.DAT_HORDING_UPDT,ahm.VAR_HORDING_OWNERSHIP,adm.VAR_DISPLAYTYPE_NAME,ahtm.VAR_HOARDINGTYPE_NAME FROM aoad_hording_mst ahm LEFT JOIN aoad_displaytype_mst adm ON adm.NUM_DISPLAYTYPE_ID=ahm.NUM_HORDING_DISPTYPEID LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE  WHERE NUM_HORDING_ID=:id AND NUM_HORDING_ULBID=:ulbId";
        internal const string Select_byId_HoardingMaster = "SELECT ahm.NUM_HORDING_ULBID, ahm.NUM_HORDING_ID, ahm.VAR_HORDING_HOLDNAME, ahm.VAR_HORDING_HOLDADDRESS, ahm.VAR_HORDING_HOLDTYPE, ahm.NUM_HORDING_DISPTYPEID, ahm.NUM_HORDING_PRABHAGID, ahm.NUM_HORDING_LOCATIONID, ahm.VAR_HORDING_LANDMARK, ahm.NUM_HORDING_LENGTH, ahm.NUM_HORDING_WIDTH, ahm.NUM_HORDING_TOTALSQFT, ahm.VAR_HORDING_ACTIVE, ahm.DAT_HORDING_INSDT, ahm.VAR_HORDING_INSBY, ahm.VAR_HORDING_UPDBY, ahm.DAT_HORDING_UPDT,ahm.VAR_HORDING_OWNERSHIP,adm.VAR_DISPLAYTYPE_NAME,ahtm.VAR_HOARDINGTYPE_NAME FROM aoad_hording_mst ahm LEFT JOIN aoad_displaytype_mst adm ON adm.NUM_DISPLAYTYPE_ID=ahm.NUM_HORDING_DISPTYPEID LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE  WHERE NUM_HORDING_ID=:id";
        internal const string ModifyStatus_HoardingMaster = "UPDATE Advertisement.aoad_hording_mst Set VAR_HORDING_ACTIVE=:VAR_HORDING_ACTIVE,VAR_HORDING_UPDBY=:VAR_HORDING_UPDBY,DAT_HORDING_UPDT=:DAT_HORDING_UPDT Where NUM_HORDING_ID=:NUM_HORDING_ID AND NUM_HORDING_ULBID=:NUM_HORDING_ULBID";
        internal const string Select_Active_DisplayTypes = "Select NUM_DISPLAYTYPE_ID,VAR_DISPLAYTYPE_NAME,VAR_DISPLAYTYPE_STATUS from aoad_displaytype_mst where VAR_DISPLAYTYPE_STATUS='A'";
        internal const string Select_DisplayTypes_Exists_Config = "Select adm.NUM_DISPLAYTYPE_ID,adm.VAR_DISPLAYTYPE_NAME,adc.NUM_DISPLAYCONFIG_DISPLAYID,adc.NUM_DISPLAYCONFIG_ID, CASE WHEN adc.NUM_DISPLAYCONFIG_DISPLAYID IS NULL THEN 0 ELSE 1 END AS ExistsInConfig from aoad_displaytype_mst adm LEFT JOIN aoad_displaytype_config adc ON adm.NUM_DISPLAYTYPE_ID = adc.NUM_DISPLAYCONFIG_DISPLAYID AND adc.NUM_DISPLAYCONFIG_ULBID=1 where adm.VAR_DISPLAYTYPE_STATUS='A'";
        internal const string ModifyStatus_LocationMaster = "UPDATE Advertisement.aoad_location_mst Set VAR_LOCATION_ACTIVE=:VAR_LOCATION_ACTIVE,VAR_LOCATION_UPDBY=:VAR_LOCATION_UPDBY,DAT_LOCATION_UPDT=:DAT_LOCATION_UPDT Where NUM_LOCATION_ID=:NUM_LOCATION_ID AND NUM_LOCATION_ULBID=:NUM_LOCATION_ULBID";
        internal const string ModifyStatus_HoardingTypeMaster = "UPDATE AOAD_HOARDINGTYPE_MST SET VAR_HOARDINGTYPE_STATUS=:VAR_HOARDINGTYPE_STATUS, VAR_HOARDINGTYPE_UPDBY=:VAR_HOARDINGTYPE_UPDBY, DAT_HOARDINGTYPE_UPDT=:DAT_HOARDINGTYPE_UPDT WHERE NUM_HOARDINGTYPE_ID=:NUM_HOARDINGTYPE_ID";

        internal const string ListItem_HordingTypes = "Select VAR_HOARDINGTYPE_NAME As DisplayName,NUM_HOARDINGTYPE_ID As Id,VAR_HOARDINGTYPE_STATUS as Active from  aoad_hoardingtype_mst where VAR_HOARDINGTYPE_STATUS='A'";
        internal const string ListItem_Locations = "select NUM_LOCATION_ID As Id, VAR_LOCATION_NAME As DisplayName from aoad_location_mst where VAR_LOCATION_ACTIVE='A' AND NUM_LOCATION_ULBID=:ulbId";
        internal const string ListItem_DisplayTypes = "Select NUM_DISPLAYTYPE_ID As Id ,VAR_DISPLAYTYPE_NAME As DisplayName,VAR_DISPLAYTYPE_STATUS as Active from aoad_displaytype_mst where VAR_DISPLAYTYPE_STATUS='A'";
        internal const string ListItem_Prabhags = "SELECT NUM_PRABHAG_ID As Id, VAR_PRABHAG_NAME As DisplayName from aoad_prabhag_mas where VAR_PRABHAG_STATUS='Y' OR VAR_PRABHAG_STATUS= 'A' AND VAR_PRABHAG_ULBID=:ulbId";
        internal const string ListItem_Locations_By_PrabhagId = "SELECT NUM_LOCATION_ID As Id,VAR_LOCATION_NAME As DisplayName FROM aoad_location_mst WHERE VAR_LOCATION_ACTIVE='A' AND NUM_LOCATION_PRABHAGID=:prabhagId AND NUM_LOCATION_ULBID=:ulbId";
        internal const string ListItem_Hording_by_LocationId = "SELECT num_hording_id AS Id,var_hording_holdname As DisplayName from aoad_hording_mst WHERE num_hording_locationid=:locationid AND var_hording_active='A' AND NUM_HORDING_ULBID=:ulbId";
       // internal const string ListItem_Locations_By_PrabhagId = "SELECT NUM_LOCATION_ID As Id,VAR_LOCATION_NAME As DisplayName FROM aoad_location_mst WHERE VAR_LOCATION_ACTIVE='A' AND NUM_LOCATION_PRABHAGID=:prabhagId";
       // internal const string ListItem_Hording_by_LocationId = "SELECT num_hording_id AS Id,var_hording_holdname As DisplayName from aoad_hording_mst WHERE num_hording_locationid=:locationid AND var_hording_active='A'";
        internal const string ListItem_Corporations = "SELECT NUM_CORPORATION_ID AS Id,VAR_CORPORATION_NAME AS DisplayName,VAR_CORPORATION_MNAME FROM admins.aoma_corporation_mas WHERE var_corporation_name IS NOT NULL";


        internal const string Application_Auth_Search = "SELECT DISTINCT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.VAR_APPLI_INSBY,apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT, apm.VAR_APPLI_UPDBY,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME,adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID WHERE apm.NUM_APPLI_LOCATIONID = :p_location_id AND apm.NUM_APPLI_PRABHAGID = :p_prabhag_id AND apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.VAR_APPLI_APPROVFLAG='P' ORDER BY apm.DAT_APPLI_INSDT DESC";
        internal const string Application_Details_By_AppliId = @"
                       SELECT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.VAR_APPLI_INSBY,apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT, apm.VAR_APPLI_UPDBY,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, 
                              ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP,
                              aprm.VAR_PRABHAG_NAME,
                              alm.VAR_LOCATION_NAME,
                              adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id ,
                              aam.BLO_APPLIDOC_IMAGE
                        FROM aoad_appli_mst apm 
                        LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID 
                        LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID
                        LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID 
                        LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID
                        LEFT JOIN aoad_applidoc_mst aam ON  aam.NUM_APPLIDOC_APPID=apm.NUM_APPLI_ID
                        WHERE  apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.NUM_APPLI_ID=:p_appli_id";
        internal const string Application_Deauth_Search = "SELECT DISTINCT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.VAR_APPLI_INSBY,apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT, apm.VAR_APPLI_UPDBY,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME,adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID WHERE apm.NUM_APPLI_LOCATIONID = :p_location_id AND apm.NUM_APPLI_PRABHAGID = :p_prabhag_id AND apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.VAR_APPLI_APPROVFLAG IN ('A','R') ORDER BY apm.DAT_APPLI_INSDT DESC";
        internal const string ApplicationClose_By_AppId = "Select NUM_APPLICLOSE_ID,NUM_APPLICLOSE_HORDING_ID,VAR_APPLICLOSE_REMARK,BLO_APPLICLOSE_IMAGE,VAR_APPLICLOSE_INSBY,DAT_APPLICLOSE_INSDT,VAR_APPLICLOSE_APPROVBY,DAT_APPLICLOSE_APPROVDT,VAR_APPLICLOSE_APPROVREMARK,VAR_APPLICLOSE_IPADDRESS,NUM_APPLICLOSE_APPID,NUM_APPLICLOSE_ULBID From aoad_appliclose_mst Where NUM_APPLICLOSE_ULBID=:NUM_APPLICLOSE_ULBID AND NUM_APPLICLOSE_APPID=:NUM_APPLICLOSE_APPID";


        internal const string Admin_Dashboard_Approval_Flag_Status =
            @"
                SELECT 
                    SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'P' THEN 1 ELSE 0 END) AS Pending,
                    SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'A' THEN 1 ELSE 0 END) AS Approved,
                    SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'R' THEN 1 ELSE 0 END) AS Rejected,
                    SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'C' THEN 1 ELSE 0 END) AS Cancelled
                FROM aoad_appli_mst apm
                WHERE apm.NUM_APPLI_ULBID = :ulbId
                GROUP BY APM.VAR_APPLI_APPROVFLAG";

        internal const string Admin_Dashboard_Prabhag_Overview =
            @"SELECT 
                APM.NUM_APPLI_PRABHAGID,aprm.VAR_PRABHAG_NAME,
                COUNT(*) AS TOTALCOUNT,
                SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'P' THEN 1 ELSE 0 END) AS Pending,
                SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'A' THEN 1 ELSE 0 END) AS SANCTION,
                SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'P' AND TO_DATE(APM.DAT_APPLI_UPTODT, 'DD-MM-YYYY') < TO_DATE(CURRENT_DATE, 'DD-MM-YYYY') THEN 1 ELSE 0 END) AS EXPIRED
               FROM aoad_appli_mst APM
               LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID
               WHERE APM.NUM_APPLI_ULBID=:ulbId
               GROUP BY APM.NUM_APPLI_PRABHAGID,aprm.VAR_PRABHAG_NAME";
    }

    //vendor Queries
    internal static partial class Queries
    {
        internal const string SP_Citzenlogin_Ins = "advertisement.aoad_Citzenlogin_ins";
        internal const string SP_CitzenRegistration_Ins = "advertisement.aoad_userregistration_ins";

        internal const string Select_P_A_R_Applications = "SELECT apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO,apm.VAR_APPLI_APPLINAME,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.VAR_APPLI_APPROVFLAG, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID WHERE  apm.NUM_APPLI_ULBID=:ulbId AND apm.VAR_APPLI_APPROVFLAG IN ('P', 'A', 'R') ORDER BY apm.NUM_APPLI_ID DESC";
        internal const string SP_AOAD_APPLI_INS = "advertisement.AOAD_APPLI_INS";
        internal const string Appli_By_Id = "SELECT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id,ahtm.VAR_HOARDINGTYPE_NAME FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID   LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID  LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE WHERE  apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.NUM_APPLI_ID=:p_appli_id";
        //internal const string Select_App_Close_Search = "SELECT apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO,apm.VAR_APPLI_APPLINAME,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.VAR_APPLI_APPROVFLAG, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME, apcm.num_appliclose_id FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID LEFT JOIN aoad_appliclose_mst apcm ON apcm.NUM_APPLICLOSE_APPID=apm.NUM_APPLI_ID WHERE  apm.NUM_APPLI_ULBID=:ulbId AND apm.VAR_APPLI_APPROVFLAG IN ('P', 'A', 'R') AND apm.NUM_APPLI_PRABHAGID=:prabhagId AND apm.NUM_APPLI_HORDINGID=:hordingId AND apm.NUM_APPLI_LOCATIONID=:locationId AND  Lower(apm.var_appli_insby)=:userId ORDER BY apm.NUM_APPLI_ID DESC";
        internal const string Select_App_Close_Search = @"
                    SELECT  apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO,apm.VAR_APPLI_APPLINAME,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.VAR_APPLI_APPROVFLAG, 
                           ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_OWNERSHIP, 
                           aprm.VAR_PRABHAG_NAME,
                           alm.VAR_LOCATION_NAME
                    FROM aoad_appli_mst apm 
                    LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID 
                    LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID 
                    LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID 
                    WHERE  apm.NUM_APPLI_ULBID=:ulbId AND apm.VAR_APPLI_APPROVFLAG IN ('P', 'A', 'R')
                           AND apm.NUM_APPLI_PRABHAGID=:prabhagId 
                           AND apm.NUM_APPLI_HORDINGID=:hordingId 
                           AND apm.NUM_APPLI_LOCATIONID=:locationId 
                           AND  Lower(apm.var_appli_insby)=:userId 
                           AND NOT EXISTS (SELECT apcm.NUM_APPLICLOSE_APPID FROM aoad_appliclose_mst apcm WHERE apm.NUM_APPLI_ID=apcm.NUM_APPLICLOSE_APPID)
                    ORDER BY apm.NUM_APPLI_ID DESC";
        internal const string Select_multi_Applications = "SELECT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.VAR_APPLI_APPLINAME,apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, apcm.num_appliclose_id FROM aoad_appli_mst apm LEFT JOIN aoad_appliclose_mst apcm ON apcm.NUM_APPLICLOSE_APPID=apm.NUM_APPLI_ID WHERE apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.NUM_APPLI_ID IN :p_appli_ids";
        internal const string SP_aoad_AppliClose_ins = "advertisement.aoad_AppliClose_ins";
        internal const string Select_P_A_R_Status_Applications = "SELECT apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO,apm.VAR_APPLI_APPLINAME,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.VAR_APPLI_APPROVFLAG, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID WHERE  apm.NUM_APPLI_ULBID=:ulbId AND apm.VAR_APPLI_APPROVFLAG IN ('P', 'A', 'R', 'C') AND apm.VAR_APPLI_APPROVFLAG=:status AND  Lower(apm.var_appli_insby)=:userId ORDER BY apm.NUM_APPLI_ID DESC";
        internal const string Select_P_A_R_C_Applications = "SELECT apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO,apm.VAR_APPLI_APPLINAME,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.VAR_APPLI_APPROVFLAG, ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_OWNERSHIP, aprm.VAR_PRABHAG_NAME,alm.VAR_LOCATION_NAME FROM aoad_appli_mst apm LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID WHERE  apm.NUM_APPLI_ULBID=:ulbId AND apm.VAR_APPLI_APPROVFLAG IN ('P', 'A', 'R', 'C') AND Lower(var_appli_insby)=:userId ORDER BY apm.NUM_APPLI_ID DESC";
        internal const string Validate_Appli_By_Id = @"
                                SELECT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, 
                                       ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, 
                                       adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id,
                                       ahtm.VAR_HOARDINGTYPE_NAME, 
                                       aprm.VAR_PRABHAG_NAME,
                                       alm.VAR_LOCATION_NAME,
                                       adacm.NUM_CORPORATION_ID,adacm.VAR_CORPORATION_NAME,adacm.BLOB_CORPORATION_IMG, 
                                       aam.BLO_APPLIDOC_IMAGE
                                FROM aoad_appli_mst apm  
                                LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID 
                                LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID  
                                LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID   
                                LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID  
                                LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE  
                                LEFT JOIN admins.aoma_corporation_mas adacm ON adacm.NUM_CORPORATION_ID=apm.NUM_APPLI_ULBID 
                                LEFT JOIN aoad_applidoc_mst aam ON  aam.NUM_APPLIDOC_APPID=apm.NUM_APPLI_ID                                 
                                WHERE apm.NUM_APPLI_ID=:p_appli_id";
        internal const string Validate_Appli_By_Id_Number = @"
                                SELECT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, 
                                       ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, 
                                       adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id,
                                       ahtm.VAR_HOARDINGTYPE_NAME, 
                                       aprm.VAR_PRABHAG_NAME,
                                       alm.VAR_LOCATION_NAME, 
                                       adacm.NUM_CORPORATION_ID,adacm.VAR_CORPORATION_NAME,adacm.BLOB_CORPORATION_IMG, 
                                       aam.BLO_APPLIDOC_IMAGE
                                FROM aoad_appli_mst apm  
                                LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID 
                                LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID  
                                LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID   
                                LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID  
                                LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE 
                                LEFT JOIN admins.aoma_corporation_mas adacm ON adacm.NUM_CORPORATION_ID=apm.NUM_APPLI_ULBID  
                                LEFT JOIN aoad_applidoc_mst aam ON  aam.NUM_APPLIDOC_APPID=apm.NUM_APPLI_ID                                
                                WHERE apm.NUM_APPLI_ID=:p_appli_id AND apm.VAR_APPLI_APPLINO=:p_app_no";

        internal const string Select_Citizen_By_Email = "SELECT NUM_CITIZENUSER_USERID, NUM_CITIZENUSER_ULBID, VAR_CITIZENUSER_EMAILID FROM aoad_citizennuser_mst Where Lower(LTRIM (RTRIM (VAR_CITIZENUSER_EMAILID)))=:VAR_CITIZENUSER_EMAILID AND NUM_CITIZENUSER_ULBID=:NUM_CITIZENUSER_ULBID";
        internal const string Verify_User_OTP = "SELECT NUM_CITIZENUSER_USERID, NUM_CITIZENUSER_ULBID, VAR_CITIZENUSER_EMAILID, NUM_CITIZENUSER_OTP FROM aoad_citizennuser_mst Where Lower(LTRIM (RTRIM (VAR_CITIZENUSER_EMAILID)))=:VAR_CITIZENUSER_EMAILID AND NUM_CITIZENUSER_OTP=:NUM_CITIZENUSER_OTP AND NUM_CITIZENUSER_USERID=:NUM_CITIZENUSER_USERID";
        internal const string Update_User_Password = "UPDATE aoad_citizennuser_mst SET NUM_CITIZENUSER_OTP=NULL, VAR_CITIZENUSER_PASSWORD=:VAR_CITIZENUSER_PASSWORD Where Lower(LTRIM (RTRIM (VAR_CITIZENUSER_EMAILID)))=:VAR_CITIZENUSER_EMAILID  AND NUM_CITIZENUSER_USERID=:NUM_CITIZENUSER_USERID AND NUM_CITIZENUSER_ULBID=:NUM_CITIZENUSER_ULBID";
        internal const string Vendor_Dashboard_Prabhag_Overview =
            @"SELECT 
                APM.NUM_APPLI_PRABHAGID,aprm.VAR_PRABHAG_NAME AS PrabhaName,
                COUNT(*) AS TOTALCOUNT,
                SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'P' THEN 1 ELSE 0 END) AS Pending,
                SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'A' THEN 1 ELSE 0 END) AS SANCTION,
                SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'P' AND TO_DATE(APM.DAT_APPLI_UPTODT, 'DD-MM-YYYY') < TO_DATE(CURRENT_DATE, 'DD-MM-YYYY') THEN 1 ELSE 0 END) AS EXPIRED
               FROM aoad_appli_mst APM
               LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID
               WHERE APM.NUM_APPLI_ULBID=:ulbId AND  Lower(APM.var_appli_insby)=:userId
               GROUP BY APM.NUM_APPLI_PRABHAGID,aprm.VAR_PRABHAG_NAME";

        internal const string Vendor_Dashboard_Application_Status =
            @"SELECT 
                SUM(CASE WHEN APM.VAR_APPLI_APPROVFLAG = 'P' AND TO_DATE(APM.DAT_APPLI_APPLIDT, 'DD-MM-YYYY')= TO_DATE(CURRENT_DATE, 'DD-MM-YYYY') THEN 1 ELSE 0 END) AS Today,
                SUM(CASE WHEN EXTRACT( MONTH FROM APM.DAT_APPLI_APPLIDT )|| '-' || EXTRACT( YEAR FROM APM.DAT_APPLI_APPLIDT )=EXTRACT( MONTH FROM CURRENT_DATE )|| '-' || EXTRACT( YEAR FROM CURRENT_DATE) THEN 1 ELSE 0 END) AS ThisMonth,
                SUM(CASE WHEN TO_DATE(APM.DAT_APPLI_APPLIDT, 'DD-MM-YYYY')>=TO_DATE(:finStart, 'DD-MM-YYYY') AND TO_DATE(APM.DAT_APPLI_APPLIDT, 'DD-MM-YYYY')<=TO_DATE(:finEnd, 'DD-MM-YYYY') THEN 1 ELSE 0 END) AS ThisYear,                
                SUM(CASE WHEN TO_DATE(APM.DAT_APPLI_FROMDT, 'DD-MM-YYYY')>=TO_DATE(:finStart, 'DD-MM-YYYY') AND TO_DATE(APM.DAT_APPLI_UPTODT, 'DD-MM-YYYY')<TO_DATE(CURRENT_DATE, 'DD-MM-YYYY') THEN 1 ELSE 0 END) AS Expired
                FROM aoad_appli_mst APM
                WHERE APM.NUM_APPLI_ULBID=:ulbId AND  Lower(APM.var_appli_insby)=:userId";


        internal const string Update_Appli_Doc = "Update aoad_applidoc_mst SET BLO_APPLIDOC_IMAGE=:BLO_APPLIDOC_IMAGE,DAT_APPLIDOC_UPDDT=TO_DATE(:DAT_APPLIDOC_UPDDT, 'YYYY-MM-DD HH24:MI:SS'), VAR_APPLIDOC_UPDBY=:VAR_APPLIDOC_UPDBY WHERE  NUM_APPLIDOC_ULBID=:NUM_APPLIDOC_ULBID AND NUM_APPLIDOC_APPID=:NUM_APPLIDOC_APPID AND NUM_APPLIDOC_ID=:NUM_APPLIDOC_ID";
        internal const string Get_Doc_APPId = "SELECT NUM_APPLIDOC_ID,NUM_APPLIDOC_ULBID,NUM_APPLIDOC_APPID,VAR_APPLIDOC_APPLINO,BLO_APPLIDOC_IMAGE,VAR_APPLIDOC_INSBY,DAT_APPLIDOC_INSDT,VAR_APPLIDOC_UPDBY,DAT_APPLIDOC_UPDDT FROM aoad_applidoc_mst where NUM_APPLIDOC_ULBID=:NUM_APPLIDOC_ULBID AND NUM_APPLIDOC_APPID=:NUM_APPLIDOC_APPID";
        internal const string Insert_Appli_Doc =
                @"INSERT INTO aoad_applidoc_mst (NUM_APPLIDOC_ULBID, NUM_APPLIDOC_APPID, VAR_APPLIDOC_APPLINO, BLO_APPLIDOC_IMAGE, VAR_APPLIDOC_INSBY, DAT_APPLIDOC_INSDT)
                  VALUES (:NUM_APPLIDOC_ULBID, :NUM_APPLIDOC_APPID, :VAR_APPLIDOC_APPLINO, :BLO_APPLIDOC_IMAGE, :VAR_APPLIDOC_INSBY, TO_DATE(:DAT_APPLIDOC_INSDT, 'YYYY-MM-DD HH24:MI:SS'))";

        internal const string Update_User_OTP = "UPDATE aoad_citizennuser_mst SET NUM_CITIZENUSER_OTP=:NUM_CITIZENUSER_OTP WHERE NUM_CITIZENUSER_USERID=:NUM_CITIZENUSER_USERID AND NUM_CITIZENUSER_ULBID=:NUM_CITIZENUSER_ULBID";
        internal const string View_Appli_By_Id =
            @"SELECT apm.NUM_APPLI_ULBID,apm.NUM_APPLI_ID,apm.VAR_APPLI_APPLINO,apm.DAT_APPLI_APPLIDT,apm.VAR_APPLI_LICENO, apm.VAR_APPLI_LICEOUTNO,apm.VAR_APPLI_APPLINAME,apm.VAR_APPLI_ADDRESS,apm.VAR_APPLI_EMAIL, apm.NUM_APPLI_MOBILENO,apm.DAT_APPLI_FROMDT,apm.DAT_APPLI_UPTODT,apm.NUM_APPLI_QTY, apm.DAT_APPLI_INSDT,apm.VAR_APPLI_APPROVFLAG,apm.DAT_APPLI_APPROVDT,apm.DAT_APPLI_UPDT,apm.VAR_APPLI_APPROVBY,apm.VAR_APPLI_APPROVREMARK, apm.NUM_APPLI_HORDINGID,apm.NUM_APPLI_PRABHAGID,apm.NUM_APPLI_LOCATIONID, 
                ahm.VAR_HORDING_HOLDNAME,ahm.VAR_HORDING_HOLDADDRESS,ahm.NUM_HORDING_LENGTH,ahm.NUM_HORDING_WIDTH,ahm.NUM_HORDING_TOTALSQFT,ahm.VAR_HORDING_OWNERSHIP, 
                adm.VAR_DISPLAYTYPE_NAME,adm.num_displaytype_id,
                ahtm.VAR_HOARDINGTYPE_NAME,
                aprm.VAR_PRABHAG_NAME, 
                alm.VAR_LOCATION_NAME
                FROM aoad_appli_mst apm 
                LEFT JOIN aoad_hording_mst ahm ON ahm.NUM_HORDING_ID = apm.NUM_APPLI_HORDINGID   
                LEFT JOIN aoad_displaytype_mst adm ON adm.num_displaytype_id=ahm.NUM_HORDING_DISPTYPEID  
                LEFT JOIN aoad_hoardingtype_mst ahtm ON ahtm.NUM_HOARDINGTYPE_ID=ahm.VAR_HORDING_HOLDTYPE 
                LEFT JOIN aoad_prabhag_mas aprm ON aprm.NUM_PRABHAG_ID=apm.NUM_APPLI_PRABHAGID
                LEFT JOIN aoad_location_mst alm ON alm.NUM_LOCATION_ID=apm.NUM_APPLI_LOCATIONID
                WHERE  apm.NUM_APPLI_ULBID=:p_ulb_id AND apm.NUM_APPLI_ID=:p_appli_id";

    }
}

