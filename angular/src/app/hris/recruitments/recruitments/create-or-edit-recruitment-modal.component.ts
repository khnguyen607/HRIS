import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { RecruitmentsServiceProxy, CreateOrEditRecruitmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditRecruitmentModal',
    templateUrl: './create-or-edit-recruitment-modal.component.html'
})
export class CreateOrEditRecruitmentModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    recruitment: CreateOrEditRecruitmentDto = new CreateOrEditRecruitmentDto();




    constructor(
        injector: Injector,
        private _recruitmentsServiceProxy: RecruitmentsServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(recruitmentId?: number): void {
    

        if (!recruitmentId) {
            this.recruitment = new CreateOrEditRecruitmentDto();
            this.recruitment.id = recruitmentId;


            this.active = true;
            this.modal.show();
        } else {
            this._recruitmentsServiceProxy.getRecruitmentForEdit(recruitmentId).subscribe(result => {
                this.recruitment = result.recruitment;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._recruitmentsServiceProxy.createOrEdit(this.recruitment)
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
