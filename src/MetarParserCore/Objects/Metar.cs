﻿using System.Collections.Generic;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// General METAR data class
    /// NOTE: Any property can be null
    /// </summary>
    public class Metar
    {
        /// <summary>
        /// Airport ICAO code
        /// </summary>
        public string Airport { get; init; }

        /// <summary>
        /// Date and time by Zulu of the observation
        /// </summary>
        public ObservationDayTime ObservationDayTime { get; init; }

        /// <summary>
        /// Current month
        /// </summary>
        public Month Month { get; init; }

        /// <summary>
        /// METAR modifier
        /// </summary>
        public MetarModifier Modifier { get; init; }

        /// <summary>
        /// Info about surface wind
        /// </summary>
        public SurfaceWind SurfaceWind { get; init; }

        /// <summary>
        /// Info about visibility
        /// </summary>
        public PrevailingVisibility PrevailingVisibility { get; init; }

        /// <summary>
        /// Info about runway visibility (RVR)
        /// </summary>
        public RunwayVisualRange RunwayVisualRange { get; init; }

        /// <summary>
        /// Special weather conditions
        /// </summary>
        public PresentWeather PresentWeather { get; init; }

        /// <summary>
        /// Info about clouds (Cloud layers)
        /// </summary>
        public CloudLayers CloudLayers { get; init; }

        /// <summary>
        /// Information about temperature
        /// </summary>
        public TemperatureInfo Temperature { get; init; }

        /// <summary>
        /// Information about air pressure
        /// </summary>
        public AltimeterSetting AltimeterSetting { get; init; }

        /// <summary>
        /// Recent weather info
        /// </summary>
        public RecentWeather RecentWeather { get; init; }

        /// <summary>
        /// Wind shear info
        /// </summary>
        public string[] WindShear { get; init; }

        /// <summary>
        /// Info about runway conditions
        /// </summary>
        public Motne Motne { get; init; }

        /// <summary>
        /// Information about changes of weather forecast
        /// </summary>
        public Trend Trend { get; init; }

        /// <summary>
        /// Additional remarks (RMK)
        /// </summary>
        public string Remarks { get; init; }

        /// <summary>
        /// Set of parse errors
        /// </summary>
        public string[] ParseErrors { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public Metar() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="currentMonth">Current month</param>
        internal Metar(Dictionary<TokenType, string[]> groupedTokens, Month currentMonth)
        {
            if (groupedTokens.Count == 0)
            {
                ParseErrors = new[] { "Grouped tokens dictionary is empty" };
                return;
            }

            var errors = new List<string>();

            Airport = getAirportIcao(groupedTokens, errors);
            ObservationDayTime = new ObservationDayTime(groupedTokens.GetTokenGroupOrDefault(TokenType.ObservationDayTime),
                errors, currentMonth);

        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get airport ICAO code
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="errors">List of parse errors</param>
        /// <returns></returns>
        private string getAirportIcao(Dictionary<TokenType, string[]> groupedTokens, List<string> errors)
        {
            var airportValue = groupedTokens.GetTokenGroupOrDefault(TokenType.Airport);
            if (airportValue.Length > 0)
                return airportValue[0];

            errors.Add("Airport ICAO code not found");
            return null;
        }

        #endregion
    }
}