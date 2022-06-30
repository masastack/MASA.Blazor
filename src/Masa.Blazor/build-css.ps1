if (Get-Command npm -ErrorAction SilentlyContinue) {
  Write-Host "Installing packages..." -ForegroundColor Yellow
  npm install

  Write-Host
  Write-Host "Building css..." -ForegroundColor Yellow
  npm run gulp

  Write-Host
  Write-Host "Builded css successfully!" -ForegroundColor Green
}
else {
  Write-Host "The npm command is not installed." -ForegroundColor Red
}