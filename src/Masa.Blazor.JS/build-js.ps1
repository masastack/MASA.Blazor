param(
  [Parameter(Mandatory = $true, HelpMessage = "main | echarts | input | markdownit | gridstack")]
  [ValidateSet("main", "echarts", "input", "markdownit", "gridstack", "activatable", "outsideclick", "baidumap")]
  [string]$file,

  [Parameter()]
  [Alias("skip")]
  [switch]$skipInstall
  )

if (Get-Command npm -ErrorAction SilentlyContinue) {
  if ($skipInstall -eq $false) {
    Write-Host "Installing packages..." -ForegroundColor Yellow
    npm install
  }

  Write-Host
  Write-Host "Building js..." -ForegroundColor Yellow
  if ($file) {
    if ($file -eq 'main') {
      npm run build
    }
    elseif ($file -eq 'input') {
      npm run build:input
    }
    elseif ($file -eq 'markdownit') {
      npm run build:markdownit
    }
    elseif ($file -eq 'gridstack') {
      npm run build:gridstack
    }
    elseif ($file -eq 'echarts') {
      npm run build:echarts
    }
    elseif ($file -eq 'activatable') {
      npm run build:activatable
    }
    elseif ($file -eq 'outsideclick') {
      npm run build:outsideclick
    }
    elseif ($file -eq 'baidumap') {
      npm run build:baidumap
    }

    Write-Host
    Write-Host "Builded js successfully!" -ForegroundColor Green
  }
}
else {
  Write-Host "The npm command is not installed." -ForegroundColor Red
}