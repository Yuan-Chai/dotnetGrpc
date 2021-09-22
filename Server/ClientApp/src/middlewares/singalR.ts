import { AnyAction } from "redux";
import { HttpTransportType, HubConnectionBuilder, LogLevel, SignalAction, SignalDispatch, signalMiddleware, withCallbacks } from "redux-signalr";
import { ApplicationState } from "../store";
import { WeatherForecast } from "../store/WeatherForecasts";

export type Action<T = void> = SignalAction<T, ApplicationState, AnyAction>;

export type Dispatch<Action extends AnyAction = AnyAction> = SignalDispatch<
  ApplicationState,
  Action
>;


const connection = new HubConnectionBuilder()
  .configureLogging(LogLevel.Debug)
  .withUrl("http://localhost:5001/hub", { 
    transport: HttpTransportType.WebSockets,
  })
  .withAutomaticReconnect()
  .build();

const callbacks = withCallbacks<Dispatch, ApplicationState>()
  .add("forecastReceived", (forecast: WeatherForecast) => (dispatch) => {
    console.log("message received", forecast);
    dispatch({ type: 'REQUEST_WEATHER_FORECASTS', forecast })
  });

export const singalR = signalMiddleware({
  callbacks,
  connection,
});