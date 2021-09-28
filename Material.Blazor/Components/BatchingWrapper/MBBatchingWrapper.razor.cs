﻿using Material.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;

namespace Material.Blazor
{
    /// <summary>
    /// This is a utility component that controls when batched JS interop calls are executed.
    /// Normally, batched JS interop calls are executed using a timer in regular intervals.
    /// In some cases, it can be an advantage to flush the batch earlier, which can be controlled with this component.
    /// 
    /// Whenever this component re-renders, the batch is flushed, hence all JS interop calls which were queued up in any child component will be executed at this point.
    /// </summary>
    public class MBBatchingWrapper : ComponentBase
    {
        [Inject] private IBatchingJSRuntime InjectedJsRuntime { get; set; }
        [CascadingParameter] private MBDialog ParentDialog { get; set; }
        

        /// <summary>
        /// The child content containing Material.Blazor components whose JS Interop calls are to be batched.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }


        /// <summary>
        /// The JS runtime selected for use by the wrapper
        /// </summary>
        protected internal IBatchingJSRuntime BatchingJsRuntime { get; set; }


        protected override void OnInitialized()
        {
            BatchingJsRuntime = ParentDialog == null ? InjectedJsRuntime : new DialogAwareBatchingJSRuntime(InjectedJsRuntime, ParentDialog);
            base.OnInitialized();
        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<MBBatchingWrapper>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<MBBatchingWrapper>.IsFixed), true);
            builder.AddAttribute(2, nameof(CascadingValue<MBBatchingWrapper>.Value), this);
            builder.AddAttribute(3, nameof(CascadingValue<MBBatchingWrapper>.ChildContent), ChildContent);
            builder.CloseComponent();
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await TriggerAsync();
        }


        /// <summary>
        /// Triggers flushing the batch.
        /// </summary>
        /// <returns></returns>
        internal async Task TriggerAsync()
        {
            await BatchingJsRuntime.FlushBatchAsync();
        }


        /// <summary>
        /// Calls <code>StateHasChanged()</code>. Used by <code>BatchingJsRuntime</code> during calls
        /// to <code>InvokeVoidAsync</code> to ensure that the batch is flushed after all items have
        /// been added to the batch.
        /// </summary>
        internal void InvokeStateHasChanged()
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }
}