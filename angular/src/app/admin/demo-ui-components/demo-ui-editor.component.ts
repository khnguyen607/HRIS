import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DemoUiComponentsServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'demo-ui-editor',
    templateUrl: './demo-ui-editor.component.html',
})
export class DemoUiEditorComponent extends AppComponentBase {
    htmlEditorInput: string;

    constructor(injector: Injector, private demoUiComponentsService: DemoUiComponentsServiceProxy) {
        super(injector);
    }

    // input mask - post
    submitValue(): void {
        this.demoUiComponentsService.sendAndGetValue(this.htmlEditorInput).subscribe((data) => {
            this.message.info(data.output, this.l('PostedValue'), { isHtml: true });
        });
    }
}
