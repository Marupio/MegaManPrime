using UnityEngine;

public interface ITime
{
    //
    // Summary:
    //     The total number of frames since the start of the game (Read Only).
    public int frameCount { get; }
    //
    // Summary:
    //     The scale at which time passes.
    public float timeScale { get; set; }
    //
    // Summary:
    //     The maximum time a frame can spend on particle updates. If the frame takes longer
    //     than this, then updates are split into multiple smaller updates.
    public float maximumParticleDeltaTime { get; set; }
    //
    // Summary:
    //     A smoothed out Time.deltaTime (Read Only).
    public float smoothDeltaTime { get; }
    //
    // Summary:
    //     The maximum value of Time.deltaTime in any given frame. This is a time in seconds
    //     that limits the increase of Time.time between two frames.
    public float maximumDeltaTime { get; set; }
    //
    // Summary:
    //     The interval in seconds at which physics and other fixed frame rate updates (like
    //     MonoBehaviour's MonoBehaviour.FixedUpdate) are performed.
    public float fixedDeltaTime { get; set; }
    //
    // Summary:
    //     The timeScale-independent interval in seconds from the last MonoBehaviour.FixedUpdate
    //     phase to the current one (Read Only).
    public float fixedUnscaledDeltaTime { get; }
    //
    // Summary:
    //     The timeScale-independent interval in seconds from the last frame to the current
    //     one (Read Only).
    public float unscaledDeltaTime { get; }
    //
    // Summary:
    //     The double precision timeScale-independent time at the beginning of the last
    //     MonoBehaviour.FixedUpdate (Read Only). This is the time in seconds since the
    //     start of the game.
    public double fixedUnscaledTimeAsDouble { get; }
    //
    // Summary:
    //     The timeScale-independent time at the beginning of the last MonoBehaviour.FixedUpdate
    //     phase (Read Only). This is the time in seconds since the start of the game.
    public float fixedUnscaledTime { get; }
    //
    // Summary:
    //     The double precision timeScale-independent time for this frame (Read Only). This
    //     is the time in seconds since the start of the game.
    public double unscaledTimeAsDouble { get; }
    //
    // Summary:
    //     The timeScale-independent time for this frame (Read Only). This is the time in
    //     seconds since the start of the game.
    public float unscaledTime { get; }
    //
    // Summary:
    //     The double precision time since the last MonoBehaviour.FixedUpdate started (Read
    //     Only). This is the time in seconds since the start of the game.
    public double fixedTimeAsDouble { get; }
    //
    // Summary:
    //     The time since the last MonoBehaviour.FixedUpdate started (Read Only). This is
    //     the time in seconds since the start of the game.
    public float fixedTime { get; }
    //
    // Summary:
    //     The interval in seconds from the last frame to the current one (Read Only).
    public float deltaTime { get; }
    //
    // Summary:
    //     The double precision time since this frame started (Read Only). This is the time
    //     in seconds since the last non-additive scene has finished loading.
    public double timeSinceLevelLoadAsDouble { get; }
    //
    // Summary:
    //     The time since this frame started (Read Only). This is the time in seconds since
    //     the last non-additive scene has finished loading.
    public float timeSinceLevelLoad { get; }
    //
    // Summary:
    //     The double precision time at the beginning of this frame (Read Only). This is
    //     the time in seconds since the start of the game.
    public double timeAsDouble { get; }
    //
    // Summary:
    //     The time at the beginning of this frame (Read Only).
    public float time { get; }
    //
    // Summary:
    //     Returns true if called inside a fixed time step callback (like MonoBehaviour's
    //     MonoBehaviour.FixedUpdate), otherwise returns false.
    public bool inFixedTimeStep { get; }

}


public class WrappedTime : ITime
{
    public int frameCount { get=>Time.frameCount; }
    public float timeScale { get=>Time.timeScale; set => Time.timeScale = value; }
    public float maximumParticleDeltaTime { get=>Time.maximumParticleDeltaTime; set => Time.maximumParticleDeltaTime = value; }
    public float smoothDeltaTime { get=>Time.smoothDeltaTime; }
    public float maximumDeltaTime { get=>Time.maximumDeltaTime; set => Time.maximumDeltaTime = value; }
    public float fixedDeltaTime { get=>Time.fixedDeltaTime; set => Time.fixedDeltaTime = value; }
    public float fixedUnscaledDeltaTime { get=>Time.fixedUnscaledDeltaTime; }
    public float unscaledDeltaTime { get=>Time.unscaledDeltaTime; }
    public double fixedUnscaledTimeAsDouble { get=>Time.fixedUnscaledTimeAsDouble; }
    public float fixedUnscaledTime { get=>Time.fixedUnscaledTime; }
    public double unscaledTimeAsDouble { get=>Time.unscaledTimeAsDouble; }
    public float unscaledTime { get=>Time.unscaledTime; }
    public double fixedTimeAsDouble { get=>Time.fixedTimeAsDouble; }
    public float fixedTime { get=>Time.fixedTime; }
    public float deltaTime { get=>Time.deltaTime; }
    public double timeSinceLevelLoadAsDouble { get=>Time.timeSinceLevelLoadAsDouble; }
    public float timeSinceLevelLoad { get=>Time.timeSinceLevelLoad; }
    public double timeAsDouble { get=>Time.timeAsDouble; }
    public float time { get=>Time.time; }
    public bool inFixedTimeStep { get=>Time.inFixedTimeStep; }
}