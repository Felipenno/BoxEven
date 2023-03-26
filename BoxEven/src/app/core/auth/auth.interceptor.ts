import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";


@Injectable({
    providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {

    constructor(
        private authService: AuthService
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(this.authService.usuarioEstaAutenticado()){
            const cloneReq = req.clone({headers: req.headers.set('Authorization',`Bearer ${this.authService.obterToken()}`)})
            return next.handle(cloneReq);
        }else {
            return next.handle(req.clone());
        }
    }
}