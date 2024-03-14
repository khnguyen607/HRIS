import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { BasicReqsesServiceProxy, CreateOrEditBasicReqsDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditBasicReqsModal',
    templateUrl: './create-or-edit-basicReqs-modal.component.html'
})
export class CreateOrEditBasicReqsModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    basicReqs: CreateOrEditBasicReqsDto = new CreateOrEditBasicReqsDto();




    constructor(
        injector: Injector,
        private _basicReqsesServiceProxy: BasicReqsesServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(basicReqsId?: number): void {
    

        if (!basicReqsId) {
            this.basicReqs = new CreateOrEditBasicReqsDto();
            this.basicReqs.id = basicReqsId;


            this.active = true;
            this.modal.show();
        } else {
            this._basicReqsesServiceProxy.getBasicReqsForEdit(basicReqsId).subscribe(result => {
                this.basicReqs = result.basicReqs;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._basicReqsesServiceProxy.createOrEdit(this.basicReqs)
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
