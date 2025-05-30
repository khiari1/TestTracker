import { BrowserCacheLocation, IPublicClientApplication, InteractionType, PublicClientApplication } from "@azure/msal-browser";
import { UserService } from "./services/user.service";
import { MsalGuardConfiguration, MsalInterceptorConfiguration } from "@azure/msal-angular";
import { environment } from "src/environments/environment.prod";

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: 'da289654-e07d-4e61-a38d-3b4a7b7fe4b9',
      authority:
        'https://login.microsoftonline.com/c882ea24-67b1-46a1-bb17-a642db996d2f',
      redirectUri: 'http://localhost:4200/',
      postLogoutRedirectUri: 'http://localhost:4200/',
      clientCapabilities: ['CP1'],
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: false, // set to true for IE 11. Remove this line to use Angular Universal
    },
  });
}


export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();

  protectedResourceMap.set(environment.baseUrl+'/', [
    'api://da289654-e07d-4e61-a38d-3b4a7b7fe4b9/access_as_user',
    'user.read',
  ]);

  return {
    interactionType: InteractionType.Popup,
    protectedResourceMap,
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Popup,
    loginFailedRoute: '/login',
  
    authRequest: {
      scopes: ['user.read'],
    },
  };
}
