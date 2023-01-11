<?php
$sub_menu = '990600';
include_once('./_common.php');
include_once(G5_EDITOR_LIB);

$html_title = "응답";

if ($w == "u")
{
    $html_title .= " 수정";
    $readonly = " readonly";

    $sql = " select * from g5_qstnanswn where qstn_no = '$qstn_no' AND answ_no = '$answ_no' ";
    $row = sql_fetch($sql);

	$ordr = $row['ordr_no'];
    if (!$row['qstn_no'])
        alert('등록된 자료가 없습니다.');
}
else
{
    $html_title .= ' 입력';

	$sql = " select MAX(ordr_no) + 1 AS ordr_no from g5_qstnanswn where qstn_no = '$qstn_no' ";

    $row = sql_fetch($sql);

	$ordr = ($row['ordr_no'] == "") ? 1 : $row['ordr_no'];
}

$g5['title'] = $html_title.' 관리';

include_once(G5_ADMIN_PATH.'/admin.head.php');
include_once(G5_PLUGIN_PATH.'/jquery-ui/datepicker.php');

auth_check($auth[$sub_menu], "w");

$token = get_admin_token();

/*
// 상단, 하단 파일경로 필드 추가
if(!sql_query(" select co_include_head from {$g5['content_table']} limit 1 ", false)) {
    $sql = " ALTER TABLE `{$g5['content_table']}`  ADD `co_include_head` VARCHAR( 255 ) NOT NULL ,
                                                    ADD `co_include_tail` VARCHAR( 255 ) NOT NULL ";
    sql_query($sql, false);
}

// html purifier 사용여부 필드
if(!sql_query(" select co_tag_filter_use from {$g5['content_table']} limit 1 ", false)) {
    sql_query(" ALTER TABLE `{$g5['content_table']}`
                    ADD `co_tag_filter_use` tinyint(4) NOT NULL DEFAULT '0' AFTER `co_content` ", true);
    sql_query(" update {$g5['content_table']} set co_tag_filter_use = '1' ");
}

// 모바일 내용 추가
if(!sql_query(" select co_mobile_content from {$g5['content_table']} limit 1", false)) {
    sql_query(" ALTER TABLE `{$g5['content_table']}`
                    ADD `co_mobile_content` longtext NOT NULL AFTER `co_content` ", true);
}

// 스킨 설정 추가
if(!sql_query(" select co_skin from {$g5['content_table']} limit 1 ", false)) {
    sql_query(" ALTER TABLE `{$g5['content_table']}`
                    ADD `co_skin` varchar(255) NOT NULL DEFAULT '' AFTER `co_mobile_content`,
                    ADD `co_mobile_skin` varchar(255) NOT NULL DEFAULT '' AFTER `co_skin` ", true);
    sql_query(" update {$g5['content_table']} set co_skin = 'basic', co_mobile_skin = 'basic' ");
}
*/

//include_once (G5_ADMIN_PATH.'/admin.head.php');
?>

<form name="frmcontentform" action="./answerformupdate.php" onsubmit="return frmcontentform_check(this);" method="post" enctype="MULTIPART/FORM-DATA" >
<input type="hidden" name="w" value="<?php echo $w; ?>">
<input type="hidden" name="co_html" value="1">
<input type="hidden" name="token" value="<?=$token?>">
<input type="hidden" name="qstn_no" value="<?=$qstn_no?>">
<input type="hidden" name="answ_no" value="<?=$row[answ_no]?>">

<div class="tbl_frm01 tbl_wrap">
    <table>
    <caption><?php echo $g5['title']; ?> 목록</caption>
    <colgroup>
        <col class="grid_4">
        <col>
    </colgroup>
    <tbody>
    <tr>
        <th scope="row"><label for="co_id">설문지코드</label></th>
        <td>
            <?=$qstn_grop_cd?>
        </td>
    </tr>
	<tr>
        <th scope="row"><label for="co_id">응답코드</label></th>
        <td>
            <?=($row['answ_no'] == "") ? help('자동생성 됩니다.') :  $row['answ_no']; ?>
        </td>
    </tr>
    <tr>
        <th scope="row"><label for="answ_cnts">응답내용</label></th>
        <td><input type="text" name="answ_cnts" value="<?php echo htmlspecialchars2($row['answ_cnts']); ?>" id="answ_cnts" required class="frm_input required" size="90"></td>
    </tr>
	<tr>
        <th scope="row"><label for="answ_valu">응답값</label></th>
        <td><input type="text" name="answ_valu" value="<?php echo htmlspecialchars2($row['answ_valu']); ?>" id="answ_valu" required class="frm_input required" size="90"></td>
    </tr>
	<tr>
        <th scope="row"><label for="hstr_cd">삭제여부</label></th>
        <td>
			<select id="hstr_cd" name="hstr_cd">
				<option <?=($row[hstr_cd]=='O')?'selected':'' ?> value="O">사용</option>
				<option <?=($row[hstr_cd]=='H')?'selected':'' ?> value="H">삭제</option>
			</select>
		</td>
    </tr>
	<tr>
        <th scope="row"><label for="ordr_no">순서</label></th>
        <td><input type="text" name="ordr_no" value="<?php echo htmlspecialchars2($ordr); ?>" id="ordr_no" required class="frm_input required" size="10"></td>
    </tr>
    </tbody>
    </table>
</div>

<div class="btn_fixed_top">
    <a href="./answerlist.php?qstn_no=<?=$qstn_no?>" class="btn btn_02">목록</a>
    <input type="submit" value="확인" class="btn btn_submit" accesskey="s">
</div>

</form>

<script>

$(function(){
    $("#vald_strt_dd, #vald_end_dd").datepicker({ changeMonth: true, changeYear: true, dateFormat: "yy-mm-dd", showButtonPanel: true, yearRange: "c-99:c+99", maxDate: "" });
});

function frmcontentform_check(f)
{
    errmsg = "";
    errfld = "";

    check_field(f.rqst_titl, "설문지 제목을 입력하세요.");

    if (errmsg != "") {
        alert(errmsg);
        errfld.focus();
        return false;
    }
    return true;
}

</script>

<?php
include_once (G5_ADMIN_PATH.'/admin.tail.php');
?>
