import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { EmployeesServiceProxy, CreateOrEditEmployeeDto ,EmployeeEmployeeWorkTypeLookupTableDto
					} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';



@Component({
    selector: 'createOrEditEmployeeModal',
    templateUrl: './create-or-edit-employee-modal.component.html'
})
export class CreateOrEditEmployeeModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    employee: CreateOrEditEmployeeDto = new CreateOrEditEmployeeDto();

    employeeWorkTypeType = '';

	allEmployeeWorkTypes: EmployeeEmployeeWorkTypeLookupTableDto[];
					

    constructor(
        injector: Injector,
        private _employeesServiceProxy: EmployeesServiceProxy,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    
    show(employeeId?: number): void {
    

        if (!employeeId) {
            this.employee = new CreateOrEditEmployeeDto();
            this.employee.id = employeeId;
            this.employeeWorkTypeType = '';


            this.active = true;
            this.modal.show();
        } else {
            this._employeesServiceProxy.getEmployeeForEdit(employeeId).subscribe(result => {
                this.employee = result.employee;

                this.employeeWorkTypeType = result.employeeWorkTypeType;


                this.active = true;
                this.modal.show();
            });
        }
        this._employeesServiceProxy.getAllEmployeeWorkTypeForTableDropdown().subscribe(result => {						
						this.allEmployeeWorkTypes = result;
					});
					
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._employeesServiceProxy.createOrEdit(this.employee)
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
