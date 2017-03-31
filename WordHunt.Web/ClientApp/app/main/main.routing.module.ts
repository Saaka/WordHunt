import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main.component';
import { MainMenuComponent } from './mainMenu/mainMenu.component';
import { TutorialComponent } from './tutorial/tutorial.component';

const routes: Routes = [
    {
        path: 'main',
        component: MainComponent,
        children: [
            {
                path: '',
                component: MainMenuComponent,
                outlet: 'main-menu-outlet'
            },
            {
                path: 'tutorial',
                component: TutorialComponent,
                outlet: 'main-menu-outlet'
            },
        ]
    },
    //{
    //    path: 'tutorial',
    //    component: MainComponent,
    //    children: [
    //        {
    //            path: '',
    //            component: TutorialComponent,
    //            outlet: 'main-menu-outlet'
    //        }
    //    ]
    //}
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MainRoutingModule { }

export const routableComponents = [
    MainComponent, MainMenuComponent, TutorialComponent
];