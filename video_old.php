<script type="text/javascript" src="jwplayer.js"></script>
<script type="text/javascript" src="script/jquery-2.0.3.min.js"></script>

<?php
include_once '../func.php';
error_reporting(0);


if($_GET['id'])
{
		$query = "SELECT * FROM Files WHERE Id = '{$_GET['id']}'";
		$videoFile_Arr = db_query($query);
		
		
		$fileType = getFileType($videoFile_Arr['Extension']);
		
		if ($fileType == 'video')
		{
		$query = "SELECT * FROM Files WHERE Id = '{$_GET['secid']}'";
		$image_Arr = db_query($query);
		$imageName = $image_Arr['Id'].'.'.$image_Arr['Extension'];
		}
		
		$query = "SELECT User_Id FROM Users2Containers WHERE Container_Id = '{$videoFile_Arr['Container_Id']}'";
		$userId_arr = db_query($query);
		
		$query = "SELECT Login FROM Users WHERE Id ='{$userId_arr['User_Id']}' ";
		$res = db_query($query);
		$container = $res['Login'];
		
		$videoName = $videoFile_Arr['Id'].'.'.$videoFile_Arr['Extension'];
		

		require_once '../Microsoft/WindowsAzure/Storage/Blob.php';
		
		$blobClient = new Microsoft_WindowsAzure_Storage_Blob(
                    "blob.core.windows.net",
                    "timvicloud",
                    "iix4qo1g7vKcagwYFoh3pGB0d0EkMSWn2x8Aztt/wb9XoFY9uYgnrGiAcoq63zo0gxcY3GcCSjdWHk3t67idIQ=="
                    );


	$sharedVideoUrl = $blobClient->generateSharedAccessUrl($container, $videoName,'b', 'r', $blobClient->isoDate(time()), $blobClient->isoDate(time() + 3000));
	if ($fileType == 'video') $sharedImageUrl = $blobClient->generateSharedAccessUrl($container, $imageName,'b', 'r', $blobClient->isoDate(time()), $blobClient->isoDate(time() + 3000));
}	
?>
<style>
  body
    {
        margin:0px;
        padding:0px;
        background-color:Black;
    }
    table
    {
        width:100%;
        height:100%;

    }
    #source
    {
        border-bottom-style:solid;
        border-bottom-width: 0px;

        border-left-style:solid;
        border-left-width: 5px;

        border-right-style:solid;
        border-right-width: 5px;
    }
</style>

<table border="0 cellspacing="0" cellpadding="0" >
<tr>
<td id="source">
<div id="myElement">Loading the player...</div>
</td>
</tr>
</table>


<script type="text/javascript">

jwplayer("myElement").setup(
	{
	    flashplayer:'jwplayer.swf',
		
		skin: "skins/lulu/lulu.xml",
		width: "100%",
	    height: "100%",
		showicons: false,
		icons: false,
		<?php if ($_GET['play'] == 'true' )
		{		
			echo "autostart: true, \n";
			echo "file: '$sharedVideoUrl', \n";
			
		} else
		{
			echo "file: '$sharedVideoUrl', \n";
			if ($fileType == 'video') echo "image: '$sharedImageUrl',\n"; 
		}
		?>
		controls: 'none',
		controlbar: 'none',
		//controlbar.idlehide: true,
    });

	function setMute(value)
	{
		jwplayer("myElement").setMute(value);
	}
	function getMute(value)
	{
		return jwplayer("myElement").getMute();
	}
	function setPosition(value)
	{
		var duration = jwplayer("myElement").getDuration();
		jwplayer("myElement").seek(duration * value);
	}
	function setPositionSimple(value)
	{
		jwplayer("myElement").seek(value);
	}
	function getPosition()
	{
		var duration = jwplayer("myElement").getDuration();
		return jwplayer("myElement").getPosition() / duration;
	}
	function getPositionSimple()
	{
		return jwplayer("myElement").getPosition();
	}
	function getDuration()
	{
		return jwplayer("myElement").getDuration();
	}
	function rewind()
	{
		var position = jwplayer("myElement").getPosition();
		jwplayer("myElement").seek(position - 2);
	}
	function fastForward()
	{
		var position = jwplayer("myElement").getPosition();
		jwplayer("myElement").seek(position + 2);
	}
	function play()
	{
		jwplayer("myElement").play(true);
	}
	function pause()
	{
		jwplayer("myElement").pause(true);
	}
	function stop()
	{
		jwplayer("myElement").stop();
	}
</script>