# Get all shader GUIDs from materials
$projectPath = "C:\Users\zim\Game making stuff\goofyhorrorgame-ZimTest\Assets"

$matFiles = Get-ChildItem -Path $projectPath -Recurse -Filter "*.mat" -File
$shaderGUIDs = @{}

foreach ($file in $matFiles) {
    $content = Get-Content -Path $file.FullName -Raw
    $matches = [regex]::Matches($content, "m_Shader:.*guid:\s*([a-f0-9]{32})")
    foreach ($match in $matches) {
        $guid = $match.Groups[1].Value
        if ($shaderGUIDs.ContainsKey($guid)) {
            $shaderGUIDs[$guid] += ", $($file.Name)"
        } else {
            $shaderGUIDs[$guid] = $file.Name
        }
    }
}

Write-Host "Unique Shader GUIDs found:"
Write-Host "========================="
foreach ($guid in ($shaderGUIDs.Keys | Sort-Object)) {
    Write-Host "  $guid"
    Write-Host "    -> $($shaderGUIDs[$guid])"
}
