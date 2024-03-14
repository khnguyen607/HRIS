import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetRecruitmentForViewDto, RecruitmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewRecruitmentModal',
    templateUrl: './view-recruitment-modal.component.html'
})
export class ViewRecruitmentModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetRecruitmentForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetRecruitmentForViewDto();
        this.item.recruitment = new RecruitmentDto();
    }

    show(item: GetRecruitmentForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
