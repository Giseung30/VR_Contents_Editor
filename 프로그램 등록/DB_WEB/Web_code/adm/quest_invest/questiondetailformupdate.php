<?php
$sub_menu = '990600';
include_once('./_common.php');

if ($w == "u" || $w == "d")
    check_demo();

if ($w == 'd')
    auth_check($auth[$sub_menu], "d");
else
    auth_check($auth[$sub_menu], "w");

check_admin_token();

$vald_strt_dd = str_replace("-", "", $vald_strt_dd);
$vald_end_dd = str_replace("-", "", $vald_end_dd);

$error_msg = '';

$sql_common = " qstn_grop_cd		= '$qstn_grop_cd',
				qstn_cnts			= '$qstn_cnts',
				answ_dvsn			= '$answ_dvsn',
				mult_yn				= '$mult_yn',
                hstr_cd		        = '$hstr_cd',
                ordr_no				= '$ordr_no',
                updtr_id            = '$member[mb_id]'";

if ($w == "")
{
    $sql = " insert g5_qstnivstn
				set rgstr_id = '$member[mb_id]',
                    $sql_common ";

    sql_query($sql);
}
else if ($w == "u")
{
    $sql = " update g5_qstnivstn
                set $sql_common
              where qstn_grop_cd = '$qstn_grop_cd' 
			    AND qstn_no		 = '$qstn_no'";
    sql_query($sql);
}
else if ($w == "d")
{
    $sql = " delete from g5_qstnivstn where qstn_grop_cd = '$qstn_grop_cd' AND qstn_no = '$qstn_no' ";
    sql_query($sql);
}

if ($w == "")
{
	$sql = " select MAX(qstn_no) AS qstn_no from g5_qstnivstn WHERE qstn_grop_cd = '$qstn_grop_cd'";
    $row = sql_fetch($sql);

	goto_url("./questiondetailform.php?w=u&amp;qstn_grop_cd=" . $qstn_grop_cd . "&amp;qstn_no=" . $row['qstn_no']);
}
else if ($w == "u")
{
    if( $error_msg ){
        alert($error_msg, "./questionform.php?w=u&amp;qstn_grop_cd=$qstn_grop_cd&amp;qstn_no=$qstn_no");
    } else {
        goto_url("./questiondetailform.php?w=u&amp;qstn_grop_cd=$qstn_grop_cd&amp;qstn_no=$qstn_no");
    }
}
else
{
    goto_url("./questiondetaillist.php?qstn_grop_cd=$qstn_grop_cd");
}
?>
