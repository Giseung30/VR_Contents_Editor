<?php
include_once('./_common.php');

define('_INDEX_', true);
if (!defined('_GNUBOARD_')) exit; // 개별 페이지 접근 불가

$g5['title'] = '검사 결과';
include_once (G5_PATH.'/head.php');

$cnt = $_GET['cnt'];
$qstn_grop_cd = $_GET['qstn_grop_cd'];
$sum = 0;
$content_no = $_GET['content_no'];

for($i=0;$i<$cnt;$i++)
{
  $sum = $sum + $_GET[$i];
}

$sql = "select play_time from g5_qstnretn
where qstn_grop_cd='$qstn_grop_cd'";

$result = sql_query($sql);
$row = sql_fetch($result);
$play_time = $row['play_time'];

$sql = " insert g5_qstnretn
set qstn_grop_cd = '$qstn_grop_cd',
retn_id = '$member[mb_nick]',
retn_value = '$sum',
content_no = '$content_no',
play_time = '$play_time'";

$result = sql_query($sql);

$return_home = "./questionlist.php?w=u&amp;&qstn_grop_cd=$qstn_grop_cd";
?>

<h1>설문에 응해주셔서 감사합니다.</h1>
<input type="button" onclick="location.href='<?php echo $return_home ?>'" value="돌아가기">


<?php echo get_paging(G5_IS_MOBILE ? $config['cf_mobile_pages'] : $config['cf_write_pages'], $page, $total_page, "{$_SERVER['SCRIPT_NAME']}?$qstr&amp;page=", "&amp;qstn_grop_cd=$qstn_grop_cd"); ?>

<?php
include_once (G5_PATH.'/tail.php');
?>
