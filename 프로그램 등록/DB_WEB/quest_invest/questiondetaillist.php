<?php
$sub_menu = '990600';
include_once('./_common.php');

auth_check($auth[$sub_menu], "r");

if(!$qstn_grop_cd)
{
	die('<meta charset="utf-8">설문 분류를 생성해주세요.');
}

/*
if( !isset($g5['content_table']) ){
    die('<meta charset="utf-8">/data/dbconfig.php 파일에 <strong>$g5[\'content_table\'] = G5_TABLE_PREFIX.\'content\';</strong> 를 추가해 주세요.');
}
//내용(컨텐츠)정보 테이블이 있는지 검사한다.
if(!sql_query(" DESCRIBE {$g5['content_table']} ", false)) {
    if(sql_query(" DESCRIBE {$g5['g5_shop_content_table']} ", false)) {
        sql_query(" ALTER TABLE {$g5['g5_shop_content_table']} RENAME TO `{$g5['content_table']}` ;", false);
    } else {
       $query_cp = sql_query(" CREATE TABLE IF NOT EXISTS `{$g5['content_table']}` (
                      `co_id` varchar(20) NOT NULL DEFAULT '',
                      `co_html` tinyint(4) NOT NULL DEFAULT '0',
                      `co_subject` varchar(255) NOT NULL DEFAULT '',
                      `co_content` longtext NOT NULL,
                      `co_hit` int(11) NOT NULL DEFAULT '0',
                      `co_include_head` varchar(255) NOT NULL,
                      `co_include_tail` varchar(255) NOT NULL,
                      PRIMARY KEY (`co_id`)
                    ) ENGINE=MyISAM DEFAULT CHARSET=utf8 ", true);

        // 내용관리 생성
        sql_query(" insert into `{$g5['content_table']}` set co_id = 'company', co_html = '1', co_subject = '회사소개', co_content= '<p align=center><b>회사소개에 대한 내용을 입력하십시오.</b></p>' ", false );
        sql_query(" insert into `{$g5['content_table']}` set co_id = 'privacy', co_html = '1', co_subject = '개인정보 처리방침', co_content= '<p align=center><b>개인정보 처리방침에 대한 내용을 입력하십시오.</b></p>' ", false );
        sql_query(" insert into `{$g5['content_table']}` set co_id = 'provision', co_html = '1', co_subject = '서비스 이용약관', co_content= '<p align=center><b>서비스 이용약관에 대한 내용을 입력하십시오.</b></p>' ", false );
    }
}
*/

$g5['title'] = '설문지 질문 목록';
include_once (G5_ADMIN_PATH.'/admin.head.php');

$sql_common = " from g5_qstnivstn a, g5_qstnivstm b WHERE b.qstn_grop_cd = $qstn_grop_cd AND a.qstn_grop_cd = b.qstn_grop_cd ";

// 테이블의 전체 레코드수만 얻음
$sql = " select count(*) as cnt " . $sql_common;
$row = sql_fetch($sql);
$total_count = $row['cnt'];

$rows = $config['cf_page_rows'];
$total_page  = ceil($total_count / $rows);  // 전체 페이지 계산
if ($page < 1) { $page = 1; } // 페이지가 없으면 첫 페이지 (1 페이지)
$from_record = ($page - 1) * $rows; // 시작 열을 구함

$sql = "select * FROM g5_qstnivstm WHERE qstn_grop_cd = $qstn_grop_cd ";
$row = sql_fetch($sql);
$titl = $row['qstn_grop_titl'];


$sql = "select * $sql_common order by ordr_no ASC limit $from_record, {$config['cf_page_rows']} ";

$result = sql_query($sql);
?>

<div class="local_ov01 local_ov">
    <?php if ($page > 1) {?><a href="<?php echo $_SERVER['SCRIPT_NAME']; ?>">처음으로</a><?php } ?>
    <span class="btn_ov01"><span class="ov_txt">전체 내용</span><span class="ov_num"> <?php echo $total_count; ?>건</span></span>
</div>

<div class="btn_fixed_top">
	<a href="./questionlist.php" class="btn btn_02">목록</a>
    <a href="./questiondetailform.php?qstn_grop_cd=<?=$qstn_grop_cd?>" class="btn btn_01">질의 추가</a>
</div>
<h4><?=$titl?></h4>
<div class="tbl_head01 tbl_wrap">
    <table>
    <caption><?php echo $g5['title']; ?> 목록</caption>
    <thead>
    <tr>
		<th scope="col">질의코드</th>
        <th scope="col">질의내용</th>
		<th scope="col">방향</th>
		<th scope="col">다수응답</th>
		<th scope="col">순서</th>
		<th scope="col">상태</th>
		<th scope="col">관리</th>
    </tr>
    </thead>
    <tbody>
    <?php for ($i=0; $row=sql_fetch_array($result); $i++) {
        $bg = 'bg'.($i%2);
    ?>
    <tr class="<?php echo $bg; ?>">
		<td class="td_left"><?php echo htmlspecialchars2($row['qstn_no']); ?></td>
        <td class="td_left"><?php echo htmlspecialchars2($row['qstn_cnts']); ?></td>
		<td class="td_center"><?=($row['answ_dvsn']=="H")?"가로":"세로"?></td>
		<td class="td_center"><?=($row['mult_yn']=="N")?"단항":"다항"?></td>
		<td class="td_center"><?php echo htmlspecialchars2($row['ordr_no']); ?></td>
		<td class="td_center"><?=($row['hstr_cd']=="O")?"사용":"삭제"?></td>
        <td class="td_mng td_mng_l">
            <a href="./questiondetailform.php?w=u&amp;qstn_grop_cd=<?php echo $row['qstn_grop_cd']; ?>&amp;qstn_no=<?php echo $row['qstn_no']; ?> " class="btn btn_03"><span class="sound_only"></span>수정</a>
			<a href="./answerlist.php?qstn_no=<?php echo $row['qstn_no']; ?>" class="btn btn_02"><span class="sound_only"></span>응답</a>
            <a href="./questiondetailformupdate.php?w=d&amp;qstn_grop_cd=<?php echo $row['qstn_grop_cd']; ?>&amp;qstn_no=<?php echo $row['qstn_no']; ?> " onclick="return delete_confirm(this);" class="btn btn_02"><span class="sound_only"></span>삭제</a>
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

<?php echo get_paging(G5_IS_MOBILE ? $config['cf_mobile_pages'] : $config['cf_write_pages'], $page, $total_page, "{$_SERVER['SCRIPT_NAME']}?$qstr&amp;page=", "&amp;qstn_grop_cd=$qstn_grop_cd"); ?>

<?php
include_once (G5_ADMIN_PATH.'/admin.tail.php');
?>
