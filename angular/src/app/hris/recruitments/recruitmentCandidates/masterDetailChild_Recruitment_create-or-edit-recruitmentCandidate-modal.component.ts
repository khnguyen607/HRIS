import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { RecruitmentCandidatesServiceProxy, CreateOrEditRecruitmentCandidateDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'masterDetailChild_Recruitment_createOrEditRecruitmentCandidateModal',
    templateUrl: './masterDetailChild_Recruitment_create-or-edit-recruitmentCandidate-modal.component.html'
})
export class MasterDetailChild_Recruitment_CreateOrEditRecruitmentCandidateModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    recruitmentCandidate: CreateOrEditRecruitmentCandidateDto = new CreateOrEditRecruitmentCandidateDto();




    constructor(
        injector: Injector,
        private _recruitmentCandidatesServiceProxy: RecruitmentCandidatesServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
                 recruitmentId: any;
             
    show(
                 recruitmentId: any,
             recruitmentCandidateId?: number): void {
    
                 this.recruitmentId = recruitmentId;
             

        if (!recruitmentCandidateId) {
            this.recruitmentCandidate = new CreateOrEditRecruitmentCandidateDto();
            this.recruitmentCandidate.id = recruitmentCandidateId;


            this.active = true;
            this.modal.show();
        } else {
            this._recruitmentCandidatesServiceProxy.getRecruitmentCandidateForEdit(recruitmentCandidateId).subscribe(result => {
                this.recruitmentCandidate = result.recruitmentCandidate;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
                this.recruitmentCandidate.recruitmentId = this.recruitmentId;
            
            this._recruitmentCandidatesServiceProxy.createOrEdit(this.recruitmentCandidate)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }













    close(): void {
        this.active = false;
        this.modal.hide();
    }
    
     ngOnInit(): void {
        
     }    
}
