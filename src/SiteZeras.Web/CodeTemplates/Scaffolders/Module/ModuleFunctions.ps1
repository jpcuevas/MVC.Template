﻿Function Scaffold-CsTemplate([String]$Template, [String]$Project, [String]$OutputPath)
{
    if ($Delete)
    {
        Delete-ProjectItem $Project "$OutputPath.cs"
        Return;
    }

    $ControllerNamespace = "SiteZeras.Controllers"
    $ControllerTestsNamespace = "SiteZeras.Tests.Unit.Controllers"
    If ($Area) { $ControllerNamespace = "SiteZeras.Controllers.$Area" }
    If ($Area) { $ControllerTestsNamespace = "SiteZeras.Tests.Unit.Controllers.$Area" }

    Add-ProjectItemViaTemplate `
        -OutputPath $OutputPath `
        -Template $Template `
        -Model @{ `
            ModelName = $Model.SubString(0, 1).ToLower() + $Model.SubString(1); `
            ControllerTestNamespace = $ControllerTestsNamespace; `
            AreaRegistration = $Area + "AreaRegistration"; `
            ControllerNamespace = $ControllerNamespace; `

            Controller = $Controller + "Controller"; `
            IValidator = "I" + $Model + "Validator"; `
            IService = "I" + $Model + "Service"; `
            Validator = $Model + "Validator"; `
            Service = $Model + "Service"; `
            View = $Model + "View"; `
            Model = $Model; `
            Area = $Area; `
        } `
        -SuccessMessage "Added $Project\{0}." `
        -TemplateFolders $TemplateFolders `
        -Project $Project
}
Function Scaffold-AreaRegistration([String]$Template, [String]$Project, [String]$OutputPath)
{
    if (!$Delete)
    {
        $ControllerNamespace = "SiteZeras.Controllers.$Area"
        $ControllerTestsNamespace = "SiteZeras.Tests.Unit.Controllers.$Area"

        Add-ProjectItemViaTemplate `
            -OutputPath $OutputPath `
            -Template $Template `
            -Model @{ `
                ControllerTestNamespace = $ControllerTestsNamespace; `
                AreaRegistration = $Area + "AreaRegistration"; `
                ControllerNamespace = $ControllerNamespace; `
                Area = $Area; `
            } `
            -SuccessMessage "Added $Project\{0}." `
            -TemplateFolders $TemplateFolders `
            -Project $Project
    }
}
Function Scaffold-CshtmlTemplate([String]$Template, [String]$Project, [String]$OutputPath)
{
    if ($Delete)
    {
        Delete-ProjectItem $Project "$OutputPath.cshtml"
        Return;
    }

    $HeaderTitle = $Controller
    if ($Area) { $HeaderTitle = $Area + $HeaderTitle }

    Add-ProjectItemViaTemplate `
        -OutputPath $OutputPath `
        -Template $Template `
        -Model @{ `
            View = $Model + "View"; `
            HeaderTitle = $HeaderTitle; `
        } `
        -SuccessMessage "Added $Project\{0}." `
        -TemplateFolders $TemplateFolders `
        -Project $Project
}

Function Scaffold-DbSet([String]$Project, [String]$Context)
{
    if (!$Delete)
    {
        $ContextClass = Get-ProjectType -Project $Project -Type $Context
        $Models = Get-PluralizedWord $Model

        Add-ClassMemberViaTemplate `
            -SuccessMessage "Added DbSet<$Model> member to $Context." `
            -TemplateFolders $TemplateFolders `
            -CodeClass $ContextClass `
            -Template "Members\DbSet" `
            -Model @{ `
                Area = $ElementArea; `
                Models = $Models; `
                Model = $Model; `
            }
    }
}

Function Delete-AreaRegistration([String]$Project, [String]$AreaPath, [String]$Path)
{
    $AreaRegistrationDir = Get-ProjectItem -Project $Project -Path $AreaPath
    If ($AreaRegistrationDir)
    {
        $ObjectCount = (Get-ChildItem $AreaRegistrationDir.FileNames(0) | Measure-Object).Count
        If ($ObjectCount -eq 1)
        {
            Delete-ProjectItem $Project "$Path.cs"
            Delete-EmptyDir $Project $AreaPath
        }
    }
}
Function Delete-ProjectItem([String]$Project, [String]$Path)
{
    $ProjectItem = Get-ProjectItem -Project $Project -Path $Path
    If ($ProjectItem)
    {
        $ProjectItem.Delete()
        Write-Host "Deleted $Project\$Path"
    }
    Else
    {
        Write-Host "$Project\$Path is missing! Skipping..." -BackgroundColor Yellow;
    }
}

Function Delete-EmptyDir([String]$Project, [String]$Dir)
{
    $ProjectItem = Get-ProjectItem -Project $Project -Path $Dir
    If ($ProjectItem -and !(Get-ChildItem $ProjectItem.FileNames(0) -force))
    {
        $ProjectItem.Delete()
        Write-Host "Deleted $Project\$Dir"
    }
}
