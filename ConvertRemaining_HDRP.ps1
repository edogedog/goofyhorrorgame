# Convert remaining URP materials to HDRP
$projectPath = "C:\Users\zim\Game making stuff\goofyhorrorgame-ZimTest\Assets"

# Shader GUID mappings
$map = @{
    # URP Standard -> HDRP Lit
    "6e4ae4064600d784cac1e41a9e6f2e59" = "46dbf2cbbaf7e4252b3451a4e8065190"
    
    # URP ParticlesUnlit -> HDRP Unlit (particles use standard unlit)
    "0406db5a14f94604a8c57ccfbc9f3b46" = "838e5f16a3c0146b4b01e08a49e8e8a6"
    
    # URP Sprite-Unlit -> HDRP Unlit (sprites in HDRP use Unlit)
    "13c02b14c4d048fa9653293d54f6e0e1" = "838e5f16a3c0146b4b01e08a49e8e8a6"
    
    # URP Mesh2D-Lit -> HDRP Lit (2D materials use standard lit)
    "4e90a8289da1f3943885176e4f83e35c" = "46dbf2cbbaf7e4252b3451a4e8065190"
    
    # URP RenderAs2D -> HDRP Lit (2D rendering)
    "74659692f6350ba46b88180d9c826630" = "46dbf2cbbaf7e4252b3451a4e8065190"
    
    # URP Mesh2D-Unlit -> HDRP Unlit
    "8c3bb6de0c0e7c047b65c077249507e5" = "838e5f16a3c0146b4b01e08a49e8e8a6"
    
    # URP Decal -> HDRP Lit (decals use Lit shader)
    "9b4e681081e2b4c469111bb649e2f7ee" = "46dbf2cbbaf7e4252b3451a4e8065190"
    
    # URP Sprite-Lit -> HDRP Lit
    "e260cfa7296ee7642b167f1eb5be5023" = "46dbf2cbbaf7e4252b3451a4e8065190"
    
    # URP SpriteMask -> HDRP Lit (sprite masks use Lit)
    "e39bd598ce13f44ba8af997d3e42cd18" = "46dbf2cbbaf7e4252b3451a4e8065190"
}

$matFiles = Get-ChildItem -Path $projectPath -Recurse -Filter "*.mat" -File
$successCount = 0

foreach ($file in $matFiles) {
    $content = Get-Content -Path $file.FullName -Raw
    
    $modified = $false
    foreach ($urpGUID in $map.Keys) {
        $hdrpGUID = $map[$urpGUID]
        if ($content -match "guid: $($urpGUID)") {
            $content = $content.Replace("guid: $($urpGUID)", "guid: $($hdrpGUID)")
            $modified = $true
        }
    }
    
    if ($modified) {
        $content | Out-File -FilePath $file.FullName -Encoding UTF8
        $successCount++
        Write-Host "Converted: $($file.Name)"
    }
}

Write-Host ""
Write-Host "=== Conversion Complete ==="
Write-Host "Successfully converted: $successCount materials"
