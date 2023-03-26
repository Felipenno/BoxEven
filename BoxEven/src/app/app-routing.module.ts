import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/auth/auth.guard';

const routes: Routes = [
  {
    path: 'usuario',
    loadChildren: () => import('./feature/usuario/usuario.module').then(m => m.UsuarioModule)
  },
  {
    path: 'sistema',
    canActivate: [AuthGuard],
    loadChildren: () => import('./feature/main.module').then(m => m.MainModule)
  },
  { path: '', redirectTo: 'usuario', pathMatch: 'full' },
  { path: '**', redirectTo: 'usuario', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
