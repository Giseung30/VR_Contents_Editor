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

$sql_common = " qstn_no				= '$qstn_no',
				answ_no				= '$answ_no',
				answ_cnts			= '$answ_cnts',
				answ_valu			= '$answ_valu',
                hstr_cd		        = '$hstr_cd',
                ordr_no				= '$ordr_no',
                updtr_id            = '$member[mb_id]'";

if ($w == "")
{
    $sql = " insert g5_qstnanswn
				set rgstr_id = '$member[mb_id]',
                    $sql_common ";

    sql_query($sql);
}
else if ($w == "u")
{
    $sql = " update g5_qstnanswn
                set $sql_common
              where qstn_no = '$qstn_no' 
			    AND answ_no		= '$answ_no'";
    sql_query($sql);
}
else if ($w == "d")
{
    $sql = " delete from g5_qstnanswn where qstn_no = '$qstn_no' AND answ_no = '$answ_no' ";
    sql_query($sql);
}

if ($w == "")
{
	$sql = " select MAX(answ_no) AS answ_no from g5_qstnanswn WHERE qstn_no = '$qstn_no'";
    $row = sql_fetch($sql);

	goto_url("./answerform.php?w=u&amp;qstn_no=" . $qstn_no . "&amp;answ_no=" . $row['answ_no']);
}
else if ($w == "u")
{
    if( $error_msg ){
        alert($error_msg, "./answerform.php?w=u&amp;qstn_no=$qstn_no&amp;answ_no=$answ_no");
    } else {
        goto_url("./answerform.php?w=u&amp;qstn_no=$qstn_no&amp;answ_no=$answ_no");
    }
}
else
{
    goto_url("./answerlist.php?qstn_no=$qstn_no");
}
?>
