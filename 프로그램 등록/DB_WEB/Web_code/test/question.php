<?php
include_once('./_common.php');

define('_INDEX_', true);
if (!defined('_GNUBOARD_')) exit; // 개별 페이지 접근 불가

$g5['title'] = '검사지';
include_once (G5_PATH.'/head.php');

$sql_common = " from g5_qstnivstn a, g5_qstnivstm b WHERE b.qstn_grop_cd = $qstn_grop_cd AND a.qstn_grop_cd = b.qstn_grop_cd ";

// 테이블의 전체 레코드수만 얻음
$sql = " select count(*) as cnt " . $sql_common;
$row = sql_fetch($sql);
$total_count = $row['cnt'];

//$rows = $config['cf_page_rows'];
//$total_page  = ceil($total_count / $rows);  // 전체 페이지 계산
//if ($page < 1) { $page = 1; } // 페이지가 없으면 첫 페이지 (1 페이지)
//$from_record = ($page - 1) * $rows; // 시작 열을 구함

$sql = "select * FROM g5_qstnivstm WHERE qstn_grop_cd = $qstn_grop_cd ";
$row = sql_fetch($sql);
$titl = $row['qstn_grop_titl'];


//$sql = "select * $sql_common order by ordr_no ASC limit $from_record, {$config['cf_page_rows']} ";
$sql = "select * $sql_common order by ordr_no ASC";

$result = sql_query($sql);
?>
<style type="text/css">
table{
  text-align: center;
	box-align: center;
}
.test{
	float: left;
	width: 50%;
	padding:0px;
}
</style>

<div class="btn_fixed_top">
	<a href="./questionlist.php" class="btn btn_02">목록</a>
</div>
<h4><?=$titl?></h4>
<form action=question_result.php>
<div class="tbl_head01 tbl_wrap">
    <table>
    <caption><?php echo $g5['title']; ?> 목록</caption>
    <thead>
			<tr>
				<th scope="col">질의코드</th>
		    <th scope="col">질의내용</th>
				<th scope="col">응답</th>
		  </tr>
		  </thead>
		  <tbody>
      <tr>
        <td>1</td>
        <td>몇번째 시나리오 입니까?</td>
        <td><input type="text" name="content_no"></td>
      </tr>
		  <?php for ($i=0; $row=sql_fetch_array($result);$i++){
		    $bg = 'bg'.($i%2);
		  ?>
		  <tr class="<?php echo $bg; ?>">
				<td class="td_left"><?php echo htmlspecialchars2($i+2); ?></td>
		  	<td class="td_left"><?php echo htmlspecialchars2($row['qstn_cnts']); ?></td>
				<td class="td_center">
					<?php
					$qstn_no = $row['qstn_no'];
					$sql_common_ans = " from g5_qstnanswn WHERE qstn_no = $qstn_no ";

					$sql_ans = " select count(*) as cnt " . $sql_common_ans;
					$row_ans = sql_fetch($sql_ans);
					$total_count_ans = $row_ans['cnt'];

					$sql_ans = " select qstn_cnts, qstn_grop_cd from g5_qstnivstn WHERE qstn_no = $qstn_no ";
					$row_ans = sql_fetch($sql_ans);
					$qstn_cnts = $row_ans['qstn_cnts'];
					$qstn_grop_cd = $row_ans['qstn_grop_cd'];

					//$rows_ans = $config['cf_page_rows'];
					//$total_page = ceil($total_count/$rows_ans);
					//if($page < 1){$page=1;}
					//$from_record = ($page-1)*$rows_ans;

					//$sql_ans = "select * $sql_common_ans order by ordr_no ASC limit $from_record, {$config['cf_page_rows']} ";
          $sql_ans = "select * $sql_common_ans order by ordr_no ASC";

					$result_ans = sql_query($sql_ans);

					for($j=0;$row_ans=sql_fetch_array($result_ans);$j++)
					{
					?>
					<div class="test"><table>
						<tr><th>
								<?php echo htmlspecialchars2($row_ans['answ_cnts']);?>
						</th></tr>
						<tr><td>
							<input type="radio" name="<?php echo htmlspecialchars2($i) ?>" value="<?php echo htmlspecialchars2($row_ans['answ_valu']);?>">
						</td></tr>
					</table></div>
					<?php
					}
					?>
				  </td>
		  </tr>
    <?php
    }
    if ($i == 0) {
        echo '<tr><td colspan="7" class="empty_table">자료가 한건도 없습니다.</td></tr>';
    }
    ?>
    </tbody>
    </table>
</div>
<input type="hidden" name="cnt" value="<?php echo $total_count ?>">
<input type="hidden" name="qstn_grop_cd" value="<?php echo $qstn_grop_cd ?>">
<input type="submit" value="완료">
</form>
<?php //echo get_paging(G5_IS_MOBILE ? $config['cf_mobile_pages'] : $config['cf_write_pages'], $page, $total_page, "{$_SERVER['SCRIPT_NAME']}?$qstr&amp;page=", "&amp;qstn_grop_cd=$qstn_grop_cd"); ?>

<?php
include_once (G5_PATH.'/tail.php');
?>
