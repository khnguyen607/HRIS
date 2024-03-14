import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { TechnicalReqsesServiceProxy, CreateOrEditTechnicalReqsDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditTechnicalReqsModal',
    templateUrl: './create-or-edit-technicalReqs-modal.component.html'
})
export class CreateOrEditTechnicalReqsModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    technicalReqs: CreateOrEditTechnicalReqsDto = new CreateOrEditTechnicalReqsDto();




    constructor(
        injector: Injector,
        private _technicalReqsesServiceProxy: TechnicalReqsesServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(technicalReqsId?: number): void {
    

        if (!technicalReqsId) {
            this.technicalReqs = new CreateOrEditTechnicalReqsDto();
            this.technicalReqs.id = technicalReqsId;


            this.active = true;
            this.modal.show();
        } else {
            this._technicalReqsesServiceProxy.getTechnicalReqsForEdit(technicalReqsId).subscribe(result => {
                this.technicalReqs = result.technicalReqs;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._technicalReqsesServiceProxy.createOrEdit(this.technicalReqs)
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
