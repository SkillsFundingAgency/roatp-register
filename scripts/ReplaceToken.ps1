Param($file, $key, $value)

$contents = cat $file
$contents -replace "@@$key@@", "$value" > $file

Write-Host "Replaced @@$key@@ with $value"