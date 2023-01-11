<?php
$sub_menu = '300911';
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

$sql_common = " qstn_grop_titl		= '$qstn_grop_titl',
                vald_strt_dd        = '$vald_strt_dd',
                vald_end_dd			= '$vald_end_dd',
				head				= '$head',
				rslt				= '$rslt',
                updtr_id            = '$member[mb_id]'";

if ($w == "")
{
    $sql = " insert g5_qstnivstm
				set rgstr_id = '$member[mb_id]',
                    $sql_common ";

    sql_query($sql);
}
else if ($w == "u")
{
    $sql = " update g5_qstnivstm
                set $sql_common
              where qstn_grop_cd = '$qstn_grop_cd' ";
    sql_query($sql);
}
else if ($w == "d")
{
    $sql = " delete from g5_qstnivstm where qstn_grop_cd = '$qstn_grop_cd' ";
    sql_query($sql);
}

if ($w == "")
{
	$sql = " select MAX(qstn_grop_cd) AS qstn_grop_cd from g5_qstnivstm ";
    $row = sql_fetch($sql);

	goto_url("./questionform.php?w=u&amp;qstn_grop_cd=" . $row['qstn_grop_cd']);
}
else if ($w == "u")
{
    if( $error_msg ){
        alert($error_msg, "./questionform.php?w=u&amp;qstn_grop_cd=$qstn_grop_cd");
    } else {
        goto_url("./questionform.php?w=u&amp;qstn_grop_cd=$qstn_grop_cd");
    }
}
else
{
    goto_url("./questionlist.php");
}
?>
