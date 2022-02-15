#!/usr/bin/env bash

set -euo pipefail

readonly _mode="${1:-}"


# Config

case "${_mode}" in
  "clean" | "package" | "local")
    ;;
  "")
    echo "ERROR: no mode provided. It must be one of [clean, package, local]"
    exit 1
    ;;
  *)
    echo "ERROR: invalid mode [${_mode}]. It must be one of [clean, package, local]"
    exit 1
    ;;
esac

case "${_mode}" in
  "clean")
    readonly _proj_path="./ReactiveTestClean"
    readonly _warnings_as_errors="True"
    ;;
  "package")
    readonly _proj_path="./ReactiveTestPackage"
    readonly _warnings_as_errors="True"
    ;;
  "local")
    readonly _proj_path="./ReactiveTestLocal"
    # NOTE(jpr): need to disable to compile ReactiveUI
    readonly _warnings_as_errors="False"
    ;;
esac


# Build

echo "Building version [${_mode}]"
echo

find "${_proj_path}" -maxdepth 1 -type d \( -name "bin" -or -name "obj" \) -exec rm -rf {} \;
if [ "local" = "${_mode}" ]; then
  find "./ReactiveUI" -maxdepth 3 -type d \( -name "bin" -or -name "obj" \) -exec rm -rf {} \;
fi

dotnet build "${_proj_path}" \
  -t:Build -c:Release -f:net6.0-ios \
  -p:Platform=iPhone -p:RuntimeIdentifier=ios-arm64 \
  -p:BuildIpa=True -p:Adhoc=True \
  -p:OutputPath='bin/Adhoc/' -p:TreatWarningsAsErrors=$_warnings_as_errors

readonly _ipa_path="${_proj_path}/bin/Adhoc/ReactiveTest.ipa"
if [ ! -f "${_ipa_path}" ]; then
    echo
    echo 'ERROR: .ipa not generated'
    exit 1
fi

echo
echo ".ipa built successfully at [${_ipa_path}]"
