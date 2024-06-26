using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace PsMoveAPI
{
    public class ControllerHelper
    {
        #region enums
        public enum PSMove_Bool
        {
            PSMove_False = 0, /*!< False, Failure, Disabled (depending on context) */
            PSMove_True = 1, /*!< True, Success, Enabled (depending on context) */
        };

        public enum PSMove_Version
        {
            /**
             * Version format: AA.BB.CC = 0xAABBCC
             *
             * Examples:
             *  3.0.1 = 0x030001
             *  4.2.11 = 0x04020B
             **/
            PSMOVE_CURRENT_VERSION = 0x030001, /*!< Current version, see psmove_init() */
        }

        // Not entirely sure why some of these buttons (R3/L3) are exposed...
        public enum PSMoveButton
        {
            L2 = 1 << 0x00,
            R2 = 1 << 0x01,
            L1 = 1 << 0x02,
            R1 = 1 << 0x03,
            Triangle = 1 << 0x04,
            Circle = 1 << 0x05,
            Cross = 1 << 0x06,
            Square = 1 << 0x07,
            Select = 1 << 0x08,
            L3 = 1 << 0x09,
            R3 = 1 << 0x0A,
            Start = 1 << 0x0B,
            Up = 1 << 0x0C,
            Right = 1 << 0x0D,
            Down = 1 << 0x0E,
            Left = 1 << 0x0F,
            PS = 1 << 0x10,
            Move = 1 << 0x13,
            Trigger = 1 << 0x14,    /* We can use this value with IsButtonDown() (or the events) to get
                             * a binary yes/no answer about if the trigger button is down at all.
                             * For the full integer/analog value of the trigger, see the corresponding property below.
                             */
        };

        // Used by psmove_get_battery().
        public enum PSMove_Battery_Level
        {
            Batt_MIN = 0x00, /*!< Battery is almost empty (< 20%) */
            Batt_20Percent = 0x01, /*!< Battery has at least 20% remaining */
            Batt_40Percent = 0x02, /*!< Battery has at least 40% remaining */
            Batt_60Percent = 0x03, /*!< Battery has at least 60% remaining */
            Batt_80Percent = 0x04, /*!< Battery has at least 80% remaining */
            Batt_MAX = 0x05, /*!< Battery is fully charged (not on charger) */
            Batt_CHARGING = 0xEE, /*!< Battery is currently being charged */
            Batt_CHARGING_DONE = 0xEF, /*!< Battery is fully charged (on charger) */
        };
        #endregion

        [DllImport("psmoveapi")]
        public static extern PSMove_Bool psmove_init(PSMove_Version version);

        [DllImport("libpsmoveapi")]
        public static extern uint psmove_poll(IntPtr move);

        //connection
        [DllImport("libpsmoveapi")]
        public static extern int psmove_pair(IntPtr move);

        [DllImport("psmoveapi.dll")]
        public static extern void psmove_disconnect(IntPtr move);

        [DllImport("libpsmoveapi")]
        public static extern int psmove_count_connected();

        [DllImport("libpsmoveapi")]
        public static extern IntPtr psmove_connect_by_id(int id);

        //buttons
        [DllImport("psmoveapi")]
        public static extern uint psmove_get_buttons(IntPtr move);

        //leds
        [DllImport("libpsmoveapi")]
        public static extern void psmove_set_leds(IntPtr move, char r, char g, char b);

        [DllImport("libpsmoveapi")]
        public static extern int psmove_update_leds(IntPtr move);

        [DllImport("libpsmoveapi")]
        public static extern PSMove_Battery_Level psmove_get_battery(IntPtr move);

        //movement data 
        [DllImport("libpsmoveapi")]
        public static extern void psmove_get_gyroscope(IntPtr move, ref int gx, ref int gy, ref int gz);

        [DllImport("libpsmoveapi")]
        public static extern void psmove_get_accelerometer(IntPtr move, ref int ax, ref int ay, ref int az);

        [DllImport("psmoveapi.dll")]
        public static extern void psmove_reset_orientation(IntPtr move);
    }
}