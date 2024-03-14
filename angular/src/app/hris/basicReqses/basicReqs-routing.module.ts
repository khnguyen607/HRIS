import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {BasicReqsesComponent} from './basicReqses.component';



const routes: Routes = [
    {
        path: '',
        component: BasicReqsesComponent,
        pathMatch: 'full'
    },
    
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class BasicReqsRoutingModule {
}
