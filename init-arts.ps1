
Set-Location -Path arts-in-action

if($args.Count -gt 0)
{
    if($args.Count -eq 2)
    {
        $YEAR=$args[0]
        $WEEK=$args[1]
    }
    else
    {
        Write-Host "AHA! Please type two params, 1st is YEAR, 2nd is WEEK." -Foreground Red
        Set-Location ..
        return
    }
}
else
{
    $YEAR=Get-Date -Format 'yyyy'
    $WEEK=get-date -UFormat %V
}

$NEW_PATH="$YEAR/week-$WEEK" 
$HAS_PATH=Test-Path $NEW_PATH
if(!$HAS_PATH)
{
    New-Item $NEW_PATH -ItemType directory
}

$ITEMS = "algorithm","review","tip","share"
foreach ($item in $ITEMS) 
{ 
    if(!(Test-Path "$NEW_PATH/$item.md"))
    {
        New-Item "$NEW_PATH/$item.md" -ItemType file
    }
}

Set-Location ..