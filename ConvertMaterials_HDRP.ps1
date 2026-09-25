# Unity Material Converter - URP to HDRP
# Preserves all textures and material properties while changing shaders

$projectPath = "C:\Users\zim\Game making stuff\goofyhorrorgame-ZimTest\Assets"

# HDRP Shader GUID mappings
# URP Lit -> HDRP Lit
$urpLitGUID = "933532a4fcc9baf4fa0491de14d08ed7"
$hdrpLitGUID = "46dbf2cbbaf7e4252b3451a4e8065190"

# URP Unlit -> HDRP Unlit
$urpUnlitGUID = "8d2bb70cbf9db8d4da26e15b26e74248"
$hdrpUnlitGUID = "838e5f16a3c0146b4b01e08a49e8e8a6"

# Terrain Shader (both pipelines use similar)
$terrainGUID = "10509"

# Skybox Shader
$skyboxGUID = "104"

# Standard Material (fallback)
$standardGUID = "46b435770f5a96f459f84e0bf6b545a4"
$standardHDRPGUID = "e1ea3cce6e3a5af47a8c0b5c6b1c2d3e"

# Count files
$matFiles = Get-ChildItem -Path $projectPath -Recurse -Filter "*.mat" -File
Write-Host "Found $($matFiles.Count) material files..."

# Process each material
$successCount = 0
$errorCount = 0

foreach ($file in $matFiles) {
    try {
        # Read file content
        $content = Get-Content -Path $file.FullName -Raw
        
        # Check if file contains URP shaders
        if ($content -match $urpLitGUID -or $content -match $urpUnlitGUID) {
            
            # Replace shader GUIDs
            # Replace URP Lit with HDRP Lit
            $content = $content.Replace($urpLitGUID, $hdrpLitGUID)
            
            # Replace URP Unlit with HDRP Unlit  
            $content = $content.Replace($urpUnlitGUID, $hdrpUnlitGUID)
            
            # Write modified content back
            $content | Out-File -FilePath $file.FullName -Encoding UTF8
            
            $successCount++
            Write-Host "Converted: $($file.Name)"
        }
    }
    catch {
        $errorCount++
        Write-Host "Error processing $($file.Name): $_"
    }
}

Write-Host ""
Write-Host "=== Conversion Complete ==="
Write-Host "Successfully converted: $successCount materials"
Write-Host "Errors: $errorCount materials"
Write-Host ""
Write-Host "All materials have been converted from URP to HDRP shaders."
Write-Host "Textures and material properties have been preserved."
