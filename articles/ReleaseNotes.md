---
uid: A.ReleaseNotes
title: ReleaseNotes
---
# Release Notes

#### [3.0.0-preview.1](https://github.com/Material-Blazor/Material.Blazor)

Released 2021-??-??

**Updates**

- The planned deprecation of MBToast is now deprecated. 

**New components**

**Breaking Changes**

**Deprecated Components**

**Known issues**

<br />

#### [2.0.0](https://github.com/Material-Blazor/Material.Blazor)
In Pre-Release Q4 2020 to Q4 2021

Released 2021-11-10

**Updates**

Material.Blazor is now built against .Net 6.0.0 GA

**New components**

**Breaking Changes**

**Deprecated Components**

- MBAnimatedNavigation is now deprecated.
- MBBatchingWrapper is now deprecated.
- MBToast is now deprecated in favor of the Material styled MBSnackbar. It will remain part of Material.Blazor 2.n, however no maintenance is planned during the Material.Blazor 2.n series of releases. It will be removed from the next major Material.Blazor release. 

**Known issues**

<br />

#### [2.0.0-rc.1](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.9)
2021-10-29

**Updates**

**New components**

**Breaking Changes**

- MBBatchingWrapper is now deprecated.

**Known issues**

#### [2.0.0-preview.9](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.9)
2021-10-20

**Updates**

**New components**

**Breaking Changes**

- Animated Navigation is deprecated. It does not function in the MCW 12.0 environment
- All parameters with the word "Supress" are renamed "Suppress" - good spelling helps.

**Known issues**

#### [2.0.0-preview.8](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.8)
2021-09-20

**Updates**

- Migrated to [Material Components Web 12.0.0](https://github.com/material-components/material-components-web/tree/v12.0.0/packages/mdc-switch).

**New components**

**Breaking Changes**

- `MBSwitch` no longer supports density, having been removed in Material Theme.
- `MBFloatingActionButton` re-implemented extended variant.

**Known issues**

#### [2.0.0-preview.7](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.7)
2021-08-19

**Updates**

- Added an internal logging service for M.B. It's logging level can be controlled using the AddMBServices service extension with the new loggingLevelServiceConfiguration parameter.

- Support PrimaryActions & SecondaryActions in MBList.

**New components**

**Breaking Changes**

- MBList Actions parameter is renamed to SecondaryActions.

- The names of the JavaScript & CSS resources have changed in this preview. See the installation article for details.

**Known issues**

- MBDatePicker does not render a value if it is started with a Value of default 

- MBBatchingWrapper does not work inside a dialog or with content that includes MBSlidingContent.
 This is an experimental component and we do not recommend its use.


#### [2.0.0-preview.6](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.6)
2021-04-26

**Updates**

- Added build task to deduplicate license comments in js file

- MBDialog initialization restructured in order to fix a race condition.

- MBSlidingContent initialization to account for 'late' creation of the Items list.

- MBTooltip anchor modified to fix a race condition

**New components**

**Breaking Changes**


#### [2.0.0-preview.5](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.5)
2021-04-19

**Updates**

- Fixed a regression in MBDatepicker where the state after user interaction was not displayed correctly.

**New components**

**Breaking Changes**


#### [2.0.0-preview.4](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.4)
2021-04-16


**Updates**

- MBDataTable enhanced with pagination and linear progress implementation - no breaking changes.
- MBGrid is now Phase 1 feature complete.
- MBSnackbar now supports a parameter of 'AdditionalClass' which is applied to the snackbar. See the Snackbar demo page for a simple example of styling for Error/Warning/Info/Success messages.
- MBToast now supports centerleft, centercenter, and centerright positions. It also had the fixed width removed and toasts adjust width dynamically.
- Material.Blazor beginning to use `ILogger` to log warnings when validating component markup to help you during app development.
- General bug fixes.
- Substantial GitHub workflow upgrade orientated towards quality assurance by @StefanLoerwald, including an automated diff of every page on the demo website resulting from a PR.


**New components**

- MBMenuSurface - a stripped back menu surface that accepts a render fragment for its contents (thanks @HannahKiekens).
- MBNumericDecimalField - a decimal variant of the existing numeric input fields.


**Breaking Changes**

- MBMenu `ToggleAsync()` is now return type `Task` rather than `Task<string>` in version 1.
- MBSelect and other single select components now allow for dynamic update of the `Items` parameter.
- MBSlider value changed to be of type decimal
- MBRadioButtonGroup `EnableTouchWrapper` obsolete and removed.
- `MBToastCloseMethod` enum renamed to `MBNotifierCloseMethod`, and enum value names have changed. This is now used by both toasts and snackbars.
- The names and content of the  css and js resources required for MaterialBlazor have changed. Please see [Installation article](xref:A.Installation) for details.
- The CSS utility class `mb-card__primary` was renamed to `mb-card__autostyled`. 

<br />


#### [2.0.0-preview.3](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.3)
2021-02-08


**Updates**

- General bug fixes.
- Additional functionality in MBGrid.


**New components**

- MBSnackbar - a core Material Theme snackbar component.

**Breaking Changes**


<br />


#### [2.0.0-preview.2](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.2)
2021-01-11


**Updates**

- General bug fixes.
- Animation improvements to MBBladeSet.
- Additional functionality in MBGrid.
- Upgrade to [Material Components Web 9.0.0](https://github.com/material-components/material-components-web/tree/v9.0.0/packages).
- Tooltips use Material Component Web 9.0.0 rather than preview packages - no breaking changes.
- MBPaginator now uses Material Component Web 9.0.0's markup, with some minor modification - no breaking changes.


**New components**

**Breaking Changes**

- CDN packages now reference material components web 9.0.0, see [Installation article](xref:A.Installation).
- If you are building a theme from material components web SASS, you need to reference version 9.0.0.


<br />


#### [2.0.0-preview.1](https://github.com/Material-Blazor/Material.Blazor/tree/2.0.0-preview.1)
2020-12-29


**Updates**

- General bug fixes.
- Material.Blazor 2.0.0 is compatible with .NET 5.0 only. If you need to build against older versions of .NET please continue to use Material.Blazor 1.1.4.


**New components**

- MBBladeSet - a blade implementation inspired by Microsoft Azure blades.
- MBChipsSelectMulti - a multi select variant of a chip set.
- MBChipsSelectSingle - a single select variant of a chip set.
- MBGrid (EXPERIMENTAL) - a capable grid component. This is not yet styled to look like Material Theme and we may or may not keep it in Material.Blazor. We recommend not using MBGrid for anything other than experiment or contributing to its development.
- MBSegmentedButtonMulti - a multi select variant of segmented buttons, which have been added to Material Components Web 9.0.0.
- MBSegmentedButtonSingle - a single select variant of segmented buttons, which have been added to Material Components Web 9.0.0.
- MBSlider - a continuous and discrete single-thumb slider.


**Breaking Changes**


<br />


#### [1.1.4](https://github.com/Material-Blazor/Material.Blazor/tree/1.1.4)
06 November 2020

- Repaired Release build.

<br />


#### [1.1.3](https://github.com/Material-Blazor/Material.Blazor/tree/1.1.3)
06 November 2020

- Removed dual runtimes due to an error in DocFx.

<br />


#### [1.1.2](https://github.com/Material-Blazor/Material.Blazor/tree/1.1.2)
02 November 2020

- Upgraded build process for DocFx.

<br />


#### [1.1.1](https://github.com/Material-Blazor/Material.Blazor/tree/1.1.1)
02 November 2020

- Upgraded build process to produce Material.Blazor with support for netstandard2.1 and net5.0.

<br />


#### [1.1.0](https://github.com/Material-Blazor/Material.Blazor/tree/1.1.0)
31 October 2020

- Added FABs (floating action buttons).
- Upgraded build process to skip node builds if npm isn't installed.
- Bug fixes.

<br />


#### [1.0.0](https://github.com/Material-Blazor/Material.Blazor/tree/1.0.0)
27 October 2020

- First full release of Material.Blazor!
- Minor changes to internal cascading values, setting `IsFixed="true"`.

<br />


#### [1.0.0-rc.6](https://github.com/Material-Blazor/Material.Blazor/tree/1.0.0-rc.6)
23 October 2020

- Rewrote two-way binding in response to [ASP.NET issue #24599](https://github.com/dotnet/aspnetcore/issues/24599#issuecomment-697588562) with Steve Sanderson's advisory note.
- Toasts dynamically respond to service configuration changes.
- Bug fixes.

<br />


#### [1.0.0-rc.5](https://github.com/Material-Blazor/Material.Blazor/tree/1.0.0-rc.5)
13 October 2020

- MS Build dependency check for SASS and Typescript files working.
- Github Actions updated to new path setting construct.
- Bug fixes.

<br />


#### [1.0.0-rc.4](https://github.com/Material-Blazor/Material.Blazor/tree/1.0.0-rc.4)
24 September 2020

- Build process uses npm directly rather than via docker (docker dependency removed).
- Typescript in localized files replaces single Javascript file.
- Themes upgraded.
- `ToastService.ShowToast` adds debug parameter, showing toast only in debug mode when true.
- Bug fixes.

<br />


#### [1.0.0-rc.3](https://github.com/Material-Blazor/Material.Blazor/tree/1.0.0-rc.3)
16 September 2020

- Material helper text added to text fields and associated derivatives.
- Blazor native validation integrated with Material Components Web's validation display mechanism.
- `HelperText`, `HelperTextPersistent` and `ValidationMessageFor` implemented on:
  - MBAutocompleteTextField
  - MBDebouncedTextField
  - MBNumericDoubleField
  - MBNumericIntField
  - MBTextArea
  - MBTextField
- Attribute splatting order checked and documentation provided for reserved attributes in relevant components' articles

<br />


#### [1.0.0-rc.2](https://github.com/Material-Blazor/Material.Blazor/tree/1.0.0-rc.2)
15 September 2020

- Corrects tooltip javascript initiation bug.
- Updates website to ASP.NET 5 rc 1.
- Re-enables autocomplete demonstration due to REGEX fix in ASP.NET 5 rc 1.
- Re-order attributes in MBButton to make `type=""` attribute splat correctly.
- Minor upgrades to red round and varied themes.

<br />


#### [1.0.0-rc.1](https://github.com/Material-Blazor/Material.Blazor/tree/1.0.0-RC.1)
14 September 2020

- First 1.0.0 release candidate having migrated from prior Blazor MDC previews.
