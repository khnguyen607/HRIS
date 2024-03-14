import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {BasicReqsRoutingModule} from './basicReqs-routing.module';
import {BasicReqsesComponent} from './basicReqses.component';
import {CreateOrEditBasicReqsModalComponent} from './create-or-edit-basicReqs-modal.component';
import {ViewBasicReqsModalComponent} from './view-basicReqs-modal.component';



@NgModule({
    declarations: [
        BasicReqsesComponent,
        CreateOrEditBasicReqsModalComponent,
        ViewBasicReqsModalComponent,
        
    ],
    imports: [AppSharedModule, BasicReqsRoutingModule , AdminSharedModule ],
    
})
export class BasicReqsModule {
}
