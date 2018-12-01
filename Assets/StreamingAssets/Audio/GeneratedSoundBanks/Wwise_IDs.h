/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMB_BIRDCALLS = 387824038U;
        static const AkUniqueID AMB_FOUNTAINLOOP = 3758534000U;
        static const AkUniqueID PLAY_AMBIENCE = 278617630U;
        static const AkUniqueID PLAY_BULLET = 808719710U;
        static const AkUniqueID PLAY_DEATH = 1172822028U;
        static const AkUniqueID PLAY_GAMEPLAY = 3737014274U;
        static const AkUniqueID PLAY_TITLESCREEN = 3027900556U;
        static const AkUniqueID PLAY_TITLESCREEN_TRANSITIONOUT = 3210172030U;
        static const AkUniqueID SFX_ENEMYFIREDEATH = 3644618883U;
        static const AkUniqueID SFX_ENEMYSWING = 3612432395U;
        static const AkUniqueID SFX_IMPACT = 2405854631U;
        static const AkUniqueID SFX_PLAYERFOOTSTEPS = 724467519U;
        static const AkUniqueID SFX_PLAYERSWING = 1118618552U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATES
        {
            static const AkUniqueID GROUP = 777429653U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID PLAYERDIED = 1886223524U;
                static const AkUniqueID TITLESCREEN = 152105657U;
            } // namespace STATE
        } // namespace GAMESTATES

        namespace LOCATION
        {
            static const AkUniqueID GROUP = 1176052424U;

            namespace STATE
            {
                static const AkUniqueID BRIDGE = 2068062714U;
                static const AkUniqueID PARK = 1610254009U;
            } // namespace STATE
        } // namespace LOCATION

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GROUNDMATERIALS
        {
            static const AkUniqueID GROUP = 1431031706U;

            namespace SWITCH
            {
                static const AkUniqueID CONCRETE = 841620460U;
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
            } // namespace SWITCH
        } // namespace GROUNDMATERIALS

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID NEW_GAME_PARAMETER = 3671138082U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID PLAYERDIED = 1886223524U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID GAMESOUNDS = 2841340823U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENCE = 85412153U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID VO = 1534528548U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID EXTERIORREVERB = 3720409019U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
