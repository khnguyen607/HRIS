import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DemoUiComponentsServiceProxy, NameValueOfString } from '@shared/service-proxies/service-proxies';
import { forEach as _forEach } from 'lodash-es';

@Component({
    selector: 'demo-ui-selection',
    templateUrl: './demo-ui-selection.component.html',
})
export class DemoUiSelectionComponent extends AppComponentBase {
    filteredCountries: NameValueOfString[];
    country: any;
    countries: NameValueOfString[] = new Array<NameValueOfString>();

    constructor(injector: Injector, private demoUiComponentsService: DemoUiComponentsServiceProxy) {
        super(injector);
    }

    // get countries
    filterCountries(event): void {
        this.demoUiComponentsService.getCountries(event.query).subscribe((countries) => {
            this.filteredCountries = countries;
        });
    }

    // single select - post
    submitSelectedCountry(): void {
        let selectedCountries = new Array<NameValueOfString>();

        selectedCountries.push(this.country);

        this.demoUiComponentsService
            .sendAndGetSelectedCountries(selectedCountries)
            .subscribe((countries: NameValueOfString[]) => {
                let message = '';

                _forEach(countries, (item) => {
                    message += `<div><strong>id</strong>: ${item.value} - <strong>name</strong>: ${item.name}</div>`;
                });

                this.message.info(message, this.l('PostedValue'), { isHtml: true });
            });
    }

    // multi select - post
    submitSelectedCountries(): void {
        this.demoUiComponentsService
            .sendAndGetSelectedCountries(this.countries)
            .subscribe((countries: NameValueOfString[]) => {
                let message = '';

                _forEach(countries, (item) => {
                    message += `<div><strong>id</strong>: ${item.value} - <strong>name</strong>: ${item.name}</div>`;
                });

                this.message.info(message, this.l('PostedValue'), { isHtml: true });
            });
    }
}
