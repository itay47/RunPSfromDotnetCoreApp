[CmdletBinding()]
param (
    [Parameter(Mandatory=$false,ValueFromPipeline=$true)]
    [String]
    $MyName
)

$v = Get-ChildItem -Path D:\ -Name
if (($null -ne $MyName) -and ("" -ne $MyName)) {
    Write-Output "name: $MyName"
}
else {
    if ($null -eq $MyName)
        {Write-Output "Parameter is null"}
    elseif ($MyName -eq "") {
        Write-Output "parameter is empty"
    }
}

Write-Output $v