import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetRecruitmentCandidateForViewDto, RecruitmentCandidateDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'masterDetailChild_Recruitment_viewRecruitmentCandidateModal',
    templateUrl: './masterDetailChild_Recruitment_view-recruitmentCandidate-modal.component.html'
})
export class MasterDetailChild_Recruitment_ViewRecruitmentCandidateModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetRecruitmentCandidateForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetRecruitmentCandidateForViewDto();
        this.item.recruitmentCandidate = new RecruitmentCandidateDto();
    }

    show(item: GetRecruitmentCandidateForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
