﻿using Material.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Material.Blazor
{
    /// <summary>
    /// A Material Theme segmented button orientated as a multi-select.
    /// </summary>
    public partial class MBChipsSelectMulti<TItem> : MultiSelectComponent<TItem, MBIconBearingSelectElement<TItem>>
    {
        /// <summary>
        /// If this component is rendered inside a single-select segmented button, add the "" class.
        /// </summary>
        [CascadingParameter] private MBChipsSelectSingle<TItem> ChipsSelectSingle { get; set; }

        /// <summary>
        /// Inclusion of touch target
        /// </summary>
        [Parameter] public bool? TouchTarget { get; set; }


        private readonly string _chipSetName = Utilities.GenerateUniqueElementName();

        private bool AppliedTouchTarget => CascadingDefaults.AppliedTouchTarget(TouchTarget);
        //private Dictionary<string, object>[] ChipAttributes { get; set; }
        //private Dictionary<string, object>[] ChipIconAttributes { get; set; }
        //private Dictionary<string, object>[] ChipSpanAttributes { get; set; }
        private ElementReference ChipsReference { get; set; }
        private MBIconBearingSelectElement<TItem>[] ItemsArray { get; set; }
        private bool IsMultiSelect { get; set; }
        private DotNetObjectReference<MBChipsSelectMulti<TItem>> ObjectReference { get; set; }


        // Would like to use <inheritdoc/> however DocFX cannot resolve to references outside Material.Blazor
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IsMultiSelect = ChipsSelectSingle == null;

            ItemsArray = Items.ToArray();

            SetComponentValue += OnValueSetCallback;
            OnDisabledSet += OnDisabledSetCallback;

            ObjectReference = DotNetObjectReference.Create(this);
        }


        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                ObjectReference?.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }


        /// <summary>
        /// For Material Theme to notify of menu item selection via JS Interop.
        /// </summary>
        [JSInvokable]
        public void NotifyMultiSelected(int[] selectedIndexes)
        {
            //var selectedIndexes = Enumerable.Range(0, selected.Length).Where(i => selected[i]);
            ComponentValue = ItemsArray.Where((item, index) => selectedIndexes.Contains(index)).Select(x => x.SelectedValue).ToArray();
        }


        /// <summary>
        /// For Material Theme to notify of menu item selection via JS Interop.
        /// </summary>
        [JSInvokable]
        public void NotifySingleSelected(int index)
        {
            ComponentValue = new TItem[] { ItemsArray[index].SelectedValue };
        }


        /// <summary>
        /// Callback for value the value setter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnValueSetCallback() => InvokeAsync(() => InvokeJsVoidAsync("MaterialBlazor.MBChipsSelectMulti.setSelected", ChipsReference, Items.Select(x => Value.Contains(x.SelectedValue)).ToArray()));


        /// <summary>
        /// Callback for value the Disabled value setter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDisabledSetCallback() => InvokeAsync(() => InvokeJsVoidAsync("MaterialBlazor.MBChipsSelectMulti.setDisabled", ChipsReference, AppliedDisabled));


        /// <inheritdoc/>
        private protected override Task InstantiateMcwComponent() => InvokeJsVoidAsync("MaterialBlazor.MBChipsSelectMulti.init", ChipsReference, IsMultiSelect, ObjectReference);


        /// <summary>
        /// Used by <see cref="MBSegmentedButtonSingle{TItem}"/> to set the value.
        /// </summary>
        /// <param name="value"></param>
        internal void SetSingleSelectValue(TItem value)
        {
            Value = new TItem[] { value };
            OnValueSetCallback();
        }
    }
}
