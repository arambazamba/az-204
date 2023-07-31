import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { ActionReducer, ActionReducerMap, MetaReducer } from '@ngrx/store';
import { environment } from '../../environments/environment';
import { AILoggerService } from '../logger/ailogger.service';
import * as fromMenu from './menu/menu.reducer';

export interface State {
  menu: fromMenu.MenuState;
}

export function debug(reducer: ActionReducer<any>): ActionReducer<any> {
  return function (state, action) {
    var ai: ApplicationInsights = AILoggerService.getInstance();
    console.log('ngrx', action.type);
    ai.trackEvent({ name: action.type, properties: action });
    return reducer(state, action);
  };
}

export const reducers: ActionReducerMap<State> = {
  menu: fromMenu.menuFeature.reducer,
};

export const metaReducers: MetaReducer<any>[] = environment.features.logging
  ? [debug]
  : [];
