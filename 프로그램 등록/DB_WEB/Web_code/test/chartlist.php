<?php
include_once('./_common.php');

define('_INDEX_', true);
if (!defined('_GNUBOARD_')) exit; // 개별 페이지 접근 불가

$g5['title'] = '차트 리스트';
include_once (G5_PATH.'/head.php');

$sql_common = " from g5_qstnretn ";

// 테이블의 전체 레코드수만 얻음
$sql = " select count(*) as cnt " . $sql_common;
$row = sql_fetch($sql);
$total_count = $row['cnt'];

$rows = $config['cf_page_rows'];
$total_page  = ceil($total_count / $rows);  // 전체 페이지 계산
if ($page < 1) { $page = 1; } // 페이지가 없으면 첫 페이지 (1 페이지)
$from_record = ($page - 1) * $rows; // 시작 열을 구함

$sql = "select * $sql_common where qstn_grop_cd = $qstn_grop_cd group by content_no order by content_no ASC limit $from_record, {$config['cf_page_rows']} ";

$result = sql_query($sql);

?>

<style type="text/css">
table{
  text-align: center;
}
</style>

<div class="tbl_head01 tbl_wrap">
  <table>
    <caption>차트 목록</caption>
    <thead>
    <tr>
        <th scope="col">검사지코드</th>
        <th scope="col">컨텐츠 번호</th>
        <th scope="col">차트</th>
    </tr>
    </thead>
    <tbody>
      <?php for ($i =0;$row=sql_fetch_array($result);$i++)
      {
        $bg='bg'.($i%2);
      ?>
      <tr class="<?php echo $bg; ?>">
      <td class="td_center">
      <?php echo $row['qstn_grop_cd'];?>
      </td>
      <td class="td_center">
      <?php echo $row['content_no'];?>
      </td>
      <td class="td_center">
      <a href="./chart.php?w=u&amp;qstn_grop_cd=<?php echo $qstn_grop_cd; ?>&amp;content_no=<?php echo $row['content_no'];?>" class="btn btn_03"><span class="sount_only"></span>차트</a>
      </td>
      </tr>
    <?php }
    if ($i == 0) {
        echo '<tr><td colspan="5" class="empty_table">자료가 한건도 없습니다.</td></tr>';
    } ?>
    </tbody>
  </table>
</div>


<?php echo get_paging(G5_IS_MOBILE ? $config['cf_mobile_pages'] : $config['cf_write_pages'], $page, $total_page, "{$_SERVER['SCRIPT_NAME']}?$qstr&amp;page=", "&amp;qstn_grop_cd=$qstn_grop_cd"); ?>

<?php
include_once (G5_PATH.'/tail.php');
?>
