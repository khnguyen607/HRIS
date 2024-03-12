import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    templateUrl: './demo-ui-components.component.html',
})
export class DemoUiComponentsComponent extends AppComponentBase {
    constructor(injector: Injector) {
        super(injector);
    }

    alertVisible = true;

    hideAlert(): void{
        this.alertVisible = false;
    }
}
