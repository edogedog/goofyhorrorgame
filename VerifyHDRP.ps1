# Verify HDRP shader conversion
$projectPath = "C:\Users\zim\Game making stuff\goofyhorrorgame-ZimTest\Assets"
$urpLitGUID = "933532a4fcc9baf4fa0491de14d08ed7"
$urpUnlitGUID = "8d2bb70cbf9db8d4da26e15b26e74248"

$matFiles = Get-ChildItem -Path $projectPath -Recurse -Filter "*.mat" -File
$remainingURP = 0

foreach ($file in $matFiles) {
    $content = Get-Content -Path $file.FullName -Raw
    if ($content -match $urpLitGUID -or $content -match $urpUnlitGUID) {
        Write-Host "Still using URP shader: $($file.Name)"
        $remainingURP++
    }
}

if ($remainingURP -eq 0) {
    Write-Host "All materials successfully converted to HDRP!"
} else {
    Write-Host "Found $remainingURP materials still using URP shaders."
}
