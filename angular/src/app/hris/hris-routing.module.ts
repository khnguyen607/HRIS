import { NgModule } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    // {
                    //     path: 'users',
                    //     loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                    //     data: { permission: 'Pages.Administration.Users' },
                    // },
                    { path: '', redirectTo: 'hostDashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'hostDashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class HrisRoutingModule {
    constructor(private router: Router) {
        router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                window.scroll(0, 0);
            }
        });
    }
}
