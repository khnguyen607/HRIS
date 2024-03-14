import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {TechnicalReqsRoutingModule} from './technicalReqs-routing.module';
import {TechnicalReqsesComponent} from './technicalReqses.component';
import {CreateOrEditTechnicalReqsModalComponent} from './create-or-edit-technicalReqs-modal.component';
import {ViewTechnicalReqsModalComponent} from './view-technicalReqs-modal.component';



@NgModule({
    declarations: [
        TechnicalReqsesComponent,
        CreateOrEditTechnicalReqsModalComponent,
        ViewTechnicalReqsModalComponent,
        
    ],
    imports: [AppSharedModule, TechnicalReqsRoutingModule , AdminSharedModule ],
    
})
export class TechnicalReqsModule {
}
